using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.Threading.Tasks;

namespace MAN.Services
{
    public static class BookReadService
    {
        static List<BookRead> BookReads { get; }
        static int nextId;
        static string filePath = "bookreads.json";

        static BookReadService()
        {
            BookReads = FileStorageUtility.LoadFromFile<BookRead>(filePath) ?? new List<BookRead>();
            nextId = BookReads.Any() ? BookReads.Max(br => br.ProfileId) + 1 : 1;
        }

        public static async Task SaveToFileAsync()
        {
            await FileStorageUtility.SaveToFileAsync(filePath, BookReads);
        }

        public static async Task AddBookReadAsync(BookRead bookRead)
        {
            bookRead.ProfileId = nextId++;
            BookReads.Add(bookRead);
            await SaveToFileAsync();
        }

        public static async Task<List<BookRead>> GetAllAsync()
        {
            return await Task.FromResult(BookReads);
        }

        public static async Task<BookRead?> GetAsync(int id)
        {
            var bookRead = BookReads.FirstOrDefault(br => br.ProfileId == id);
            return await Task.FromResult(bookRead);
        }
    }
}