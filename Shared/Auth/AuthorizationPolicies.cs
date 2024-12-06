using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;

namespace MAN.Shared.Auth;
public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeAdmin", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Admin"));

            options.AddPolicy("MustBeWorker", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Worker"));

            options.AddPolicy("MustBeMember", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "User"));

    
        });
    }
}
