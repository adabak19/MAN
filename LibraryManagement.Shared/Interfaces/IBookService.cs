using LibraryManagement.Shared.Models;
namespace LibraryManagement.Shared.Interfaces;
public interface IBookService
{
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetAsyncById(int id);
    Task<Book> Add(Book book);
    Task Delete(int id);
    Task Update(Book book);


        // New method for searching books
        Task<List<object>> SearchBooksAsync(string? title, string? author, string? genre);
    }
