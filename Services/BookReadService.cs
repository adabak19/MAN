using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.IO;
using System.Text.Json;

namespace MAN.Services
{
    public static class BookReadService
    {
        static List<BookRead> BookReads { get; }
        static int nextId;
        static string filePath = "bookreads.json";

        static BookReadService()
        {
            BookReads = LoadFromFile();
            nextId = BookReads.Any() ? BookReads.Max(br => br.ProfileId) + 1 : 1;
        }

        public static List<BookRead> GetAll() => BookReads;

        public static BookRead? Get(int profileId, int bookId) =>
            BookReads.FirstOrDefault(br => br.ProfileId == profileId && br.BookId == bookId);

        public static void Add(BookRead bookRead)
        {
            BookReads.Add(bookRead);
            SaveToFile();
        }

        public static void Delete(int profileId, int bookId)
        {
            var bookRead = Get(profileId, bookId);
            if (bookRead is null)
                return;

            BookReads.Remove(bookRead);
            SaveToFile();
        }

        public static void Update(BookRead bookRead)
        {
            var index = BookReads.FindIndex(br => br.ProfileId == bookRead.ProfileId && br.BookId == bookRead.BookId);
            if (index == -1)
                return;

            BookReads[index] = bookRead;
            SaveToFile();
        }

        private static List<BookRead> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return new List<BookRead>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<BookRead>>(json) ?? new List<BookRead>();
        }

        private static void SaveToFile()
        {
            var json = JsonSerializer.Serialize(BookReads, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}