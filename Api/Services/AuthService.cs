using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;

namespace MAN.Api.Services;
public class AuthService : IAuthServiceAPI
{

    public async Task<Profile> ValidateUser(string profilename, string password)
{
    using ApplicationDbContext context = new();

    // Use ToLower() for case-insensitive comparison
    Profile? existingProfile = await context.Profiles
        .FirstOrDefaultAsync(p => p.ProfileName.ToLower() == profilename.ToLower())
        ?? throw new Exception("User not found");

    if (!existingProfile.Password.Equals(password))
    {
        throw new Exception("Password mismatch");
    }

    return existingProfile;
}

    public Task RegisterUser(Profile profile)
    {
        using ApplicationDbContext context = new();

        if (string.IsNullOrEmpty(profile.ProfileName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(profile.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here

        // save to persistence instead of list

        context.Profiles.Add(profile);

        return Task.CompletedTask;
    }
}
