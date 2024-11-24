using LibraryManagement.Shared.Models;
namespace LibraryManagement.Shared.Interfaces;
public interface IAuthorService
{
    Task<List<Author>> GetAllAsync();
    Task<Author?> GetAsyncById(int id);
    Task<Author> Add(Author author);
    Task Delete(int id);
    Task Update(Author author);
}