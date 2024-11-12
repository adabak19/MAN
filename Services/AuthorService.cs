using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.IO;
using System.Text.Json;

namespace MAN.Services
{
    public static class AuthorService
    {
        static List<Author> Authors { get; }
        static int nextId;
        static string filePath = "authors.json";

        static AuthorService()
        {
            Authors = LoadFromFile();
            nextId = Authors.Any() ? Authors.Max(a => a.Id) + 1 : 1;
        }

        public static List<Author> GetAll() => Authors;

        public static Author? Get(int id) => Authors.FirstOrDefault(a => a.Id == id);

        public static void Add(Author author)
        {
            author.Id = nextId++;
            Authors.Add(author);
            SaveToFile();
        }

        public static void Delete(int id)
        {
            var author = Get(id);
            if (author is null)
                return;

            Authors.Remove(author);
            SaveToFile();
        }

        public static void Update(Author author)
        {
            var index = Authors.FindIndex(a => a.Id == author.Id);
            if (index == -1)
                return;

            Authors[index] = author;
            SaveToFile();
        }

        private static List<Author> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return new List<Author>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Author>>(json) ?? new List<Author>();
        }

        private static void SaveToFile()
        {
            var json = JsonSerializer.Serialize(Authors, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}