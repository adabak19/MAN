using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface IAuthorService
{
    Task<List<Author>> GetAllAsync();
    Task<Author?> GetAsyncById(int id);
    Task<Author> Add(Author author);
    Task Delete(int id);
    Task Update(Author author);
}