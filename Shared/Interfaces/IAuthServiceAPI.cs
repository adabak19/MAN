using MAN.Shared.Models;

namespace MAN.Shared.Interfaces;
public interface IAuthServiceAPI
{
    Task<Profile> ValidateUser(string profilename, string password);
    Task RegisterUser(Profile profile);
}
