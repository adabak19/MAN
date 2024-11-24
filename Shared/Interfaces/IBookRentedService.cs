using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface IBookRentedService
{
    Task<List<BookRented>> GetAllAsync();
    Task<BookRented?> GetAsyncById(int profileId, int bookId);
    Task<BookRented> Add(BookRented bookRented);
    Task Delete(int profileId, int bookId);
    Task Update(BookRented bookRented);
}