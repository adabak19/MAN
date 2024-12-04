using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MAN.Client.Services;
using MAN.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace MAN.Client.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly IAuthServiceWEB _authService;

    public CustomAuthProvider(IAuthServiceWEB authService)
    {
        _authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await _authService.GetAuthAsync();

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