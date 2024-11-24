using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface IPublisherService
{
    Task<List<Publisher>> GetAllAsync();
    Task<Publisher?> GetAsyncById(int id);
    Task<Publisher> AddAsync(Publisher publisher);
    Task DeleteAsync(int id);
    Task UpdateAsync(Publisher publisher);
}