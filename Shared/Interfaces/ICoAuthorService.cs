using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface ICoAuthorService
{
    Task<List<CoAuthors>> GetAllAsync();
    Task<CoAuthors?> GetAsyncById(int bookId, int authorId);
    Task<CoAuthors> Add(CoAuthors coAuthor);
    Task Delete(int bookId, int authorId);
    Task Update(CoAuthors coAuthor);
}