using LibraryManagement.Shared.Models;
namespace LibraryManagement.Shared.Interfaces;
public interface IGenreService
{
    Task<List<Genre>> GetAllAsync();
    Task<Genre?> GetAsyncById(int id);
    Task<Genre> Add(Genre genre);
    Task Delete(int id);
    Task Update(Genre genre);
}