using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;
public interface IProfileService
{
    Task<List<Profile>> GetAllAsync();
    Task<Profile?> GetAsyncById(int id);
    Task<Profile> AddAsync(Profile profile);
    Task DeleteAsync(int id);
    Task UpdateAsync(Profile profile);
}