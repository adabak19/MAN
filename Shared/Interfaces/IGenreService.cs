using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface IGenreService
{
    Task<List<Genre>> GetAllAsync();
    Task<Genre?> GetAsyncById(int id);
    Task<Genre> Add(Genre genre);
    Task Delete(int id);
    Task Update(Genre genre);
}