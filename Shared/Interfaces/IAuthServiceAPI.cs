using MAN.Shared.Models;

namespace MAN.Shared.Interfaces;
public interface IAuthServiceAPI
{
    Task<Profile> ValidateUser(string profilename, string password);
    Task<bool> RegisterUserAsync(Profile profile);
}
