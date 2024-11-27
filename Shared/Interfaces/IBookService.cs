using MAN.Shared.DTO;
using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface IBookService
{
    Task<List<BookDto>> GetAllAsync();
    Task<BookDto?> GetAsyncById(int id);
    Task<Book> Add(Book book);
    Task Delete(int id);
    Task Update(Book book);


        // New method for searching books
        Task<List<BookDto>> SearchBooksAsync(string? title, string? author, string? genre);
    }
