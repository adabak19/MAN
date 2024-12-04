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

        Profile? existingProfile = await context.Profiles
            .FirstOrDefaultAsync(p => p.ProfileName == profilename)
            ?? throw new Exception("User not found");

        if (!existingProfile.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingProfile);
    }

    public async Task<bool> RegisterUserAsync(Profile profile)
    {
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

        try
        {
            using var context = new ApplicationDbContext();

            // Check if the username already exists
            if (await context.Profiles.AnyAsync(p => p.ProfileName == profile.ProfileName))
            {
                throw new ValidationException("Username already exists");
            }

            // Add the profile to the database
            context.Profiles.Add(profile);
            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering user: {ex.Message}");
            return false;
        }
    }

}
