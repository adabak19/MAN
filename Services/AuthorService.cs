using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAN.Services
{
    public static class AuthorService
    {
        static List<Author> Authors { get; }
        static int nextId;
        static string filePath = "authors.json";

        static AuthorService()
        {
            Authors = FileStorageUtility.LoadFromFile<Author>(filePath) ?? new List<Author>();
            nextId = Authors.Any() ? Authors.Max(a => a.Id) + 1 : 1;
        }

        public static async Task SaveToFileAsync()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(Authors, options);
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task AddAuthorAsync(Author author)
        {
            author.Id = nextId++;
            Authors.Add(author);
            await SaveToFileAsync();
        }

        public static async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await Task.FromResult(Authors);
        }

        public static async Task<Author?> GetAuthorByIdAsync(int id)
        {
            var author = Authors.FirstOrDefault(a => a.Id == id);
            return await Task.FromResult(author);
        }
    }
}