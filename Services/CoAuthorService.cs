using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.IO;
using System.Text.Json;

namespace MAN.Services
{
    public static class CoAuthorService
    {
        static List<CoAuthors> CoAuthorsList { get; }
        static int nextId;
        static string filePath = "coauthors.json";

        static CoAuthorService()
        {
            CoAuthorsList = LoadFromFile();
            nextId = CoAuthorsList.Any() ? CoAuthorsList.Max(ca => ca.BookId) + 1 : 1;
        }

        public static List<CoAuthors> GetAll() => CoAuthorsList;

        public static CoAuthors? Get(int bookId, int authorId) =>
            CoAuthorsList.FirstOrDefault(ca => ca.BookId == bookId && ca.AuthorId == authorId);

        public static void Add(CoAuthors coAuthor)
        {
            CoAuthorsList.Add(coAuthor);
            SaveToFile();
        }

        public static void Delete(int bookId, int authorId)
        {
            var coAuthor = Get(bookId, authorId);
            if (coAuthor is null)
                return;

            CoAuthorsList.Remove(coAuthor);
            SaveToFile();
        }

        public static void Update(CoAuthors coAuthor)
        {
            var index = CoAuthorsList.FindIndex(ca => ca.BookId == coAuthor.BookId && ca.AuthorId == coAuthor.AuthorId);
            if (index == -1)
                return;

            CoAuthorsList[index] = coAuthor;
            SaveToFile();
        }

        private static List<CoAuthors> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return new List<CoAuthors>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<CoAuthors>>(json) ?? new List<CoAuthors>();
        }

        private static void SaveToFile()
        {
            var json = JsonSerializer.Serialize(CoAuthorsList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}