using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface IBookGenreService
{
    Task<List<BookGenre>> GetAllAsync();
    Task<BookGenre?> GetAsyncById(int bookId, int genreId);
    Task<BookGenre> Add(BookGenre bookGenre);
    Task Delete(int bookId, int genreId);
    Task Update(BookGenre bookGenre);
}