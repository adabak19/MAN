using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface IBookReadService
{
    Task<List<BookRead>> GetAllAsync();
    Task<BookRead?> GetAsyncById(int profileId, int bookId);
    Task<BookRead> Add(BookRead bookRead);
    Task Delete(int profileId, int bookId);
    Task Update(BookRead bookRead);
}