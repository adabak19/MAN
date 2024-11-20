using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.Threading.Tasks;

namespace MAN.Services
{
    public static class BookService
    {
        static List<Book> Books { get; }
        static int nextId;
        static string filePath = "books.json";

        static BookService()
        {
            Books = FileStorageUtility.LoadFromFile<Book>(filePath) ?? new List<Book>();
            nextId = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
        }

        public static async Task SaveToFileAsync()
        {
            await FileStorageUtility.SaveToFileAsync(filePath, Books);
        }

        public static async Task AddBookAsync(Book book)
        {
            book.Id = nextId++;
            Books.Add(book);
            await SaveToFileAsync();
        }

        public static async Task<List<Book>> GetAllAsync()
        {
            return await Task.FromResult(Books);
        }

        public static async Task<Book?> GetAsync(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            return await Task.FromResult(book);
        }
    }
}