using System.Security.Claims;
using MAN.Client.Services;
using MAN.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace MAN.Client.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly IAuthServiceWEB authService;

    public CustomAuthProvider(IAuthServiceWEB authService)
    {
        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authService.GetAuthAsync();

        return new AuthenticationState(principal);
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}