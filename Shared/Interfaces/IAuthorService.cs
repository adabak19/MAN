using MAN.Shared.Models;
using MAN.Shared.DTO;
namespace MAN.Shared.Interfaces;
public interface IAuthorService
{
    Task<List<AuthorDto>> GetAllAsync();
    Task<List<Author>> GetAllAuthors();
    Task<AuthorDto?> GetAsyncById(int id);
    Task<Author> Add(Author author);
    Task Delete(int id);
    Task Update(Author author);

}