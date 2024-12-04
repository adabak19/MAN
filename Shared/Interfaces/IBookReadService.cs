using MAN.Shared.Models;
using MAN.Shared.DTO;
namespace MAN.Shared.Interfaces;
public interface IBookReadService
{
    Task<List<BookRead>> GetAllAsync();
    Task<BookRead?> GetAsyncById(int profileId, int bookId);
    Task<BookRead> Add(BookRead bookRead);
    Task Delete(int profileId, int bookId);
    Task Update(BookRead bookRead);
    Task<List<BookReadDto>> GetAsyncByBookId(int bookId);
    Task<List<BookReadDto>> GetAsyncByProfileId(int profileId);
    
    Task<List<BookReadDto>> GetAllReading();

}