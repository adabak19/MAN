using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.Threading.Tasks;

namespace MAN.Services
{
    public static class CoAuthorService
    {
        static List<CoAuthors> CoAuthorsList { get; }
        static int nextId;
        static string filePath = "coauthors.json";

        static CoAuthorService()
        {
            CoAuthorsList = FileStorageUtility.LoadFromFile<CoAuthors>(filePath) ?? new List<CoAuthors>();
            nextId = CoAuthorsList.Any() ? CoAuthorsList.Max(ca => ca.BookId) + 1 : 1;
        }

        public static async Task SaveToFileAsync()
        {
            await FileStorageUtility.SaveToFileAsync(filePath, CoAuthorsList);
        }

        public static async Task AddCoAuthorAsync(CoAuthors coAuthor)
        {
            coAuthor.BookId = nextId++;
            CoAuthorsList.Add(coAuthor);
            await SaveToFileAsync();
        }

        public static async Task<List<CoAuthors>> GetAllAsync()
        {
            return await Task.FromResult(CoAuthorsList);
        }

        public static async Task<CoAuthors?> GetAsync(int id)
        {
            var coAuthor = CoAuthorsList.FirstOrDefault(ca => ca.BookId == id);
            return await Task.FromResult(coAuthor);
        }
    }
}