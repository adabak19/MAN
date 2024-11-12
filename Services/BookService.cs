using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.IO;
using System.Text.Json;

namespace MAN.Services
{
    public static class BookService
    {
        static List<Book> Books { get; }
        static int nextId;
        static string filePath = "books.json";

        static BookService()
        {
            Books = LoadFromFile();
            nextId = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
        }

        public static List<Book> GetAll() => Books;

        public static Book? Get(int id) => Books.FirstOrDefault(b => b.Id == id);

        public static void Add(Book book)
        {
            book.Id = nextId++;
            Books.Add(book);
            SaveToFile();
        }

        public static void Delete(int id)
        {
            var book = Get(id);
            if (book is null)
                return;

            Books.Remove(book);
            SaveToFile();
        }

        public static void Update(Book book)
        {
            var index = Books.FindIndex(b => b.Id == book.Id);
            if (index == -1)
                return;

            Books[index] = book;
            SaveToFile();
        }

        private static List<Book> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return new List<Book>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        private static void SaveToFile()
        {
            var json = JsonSerializer.Serialize(Books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}