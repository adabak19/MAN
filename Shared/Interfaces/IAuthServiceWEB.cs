using System.Security.Claims;
using MAN.Shared.Models;
namespace MAN.Shared.Interfaces;

public interface IAuthServiceWEB
{
    public Task LoginAsync(string profilename, string password);
    public Task LogoutAsync();
    public Task RegisterAsync(Profile profile);
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}