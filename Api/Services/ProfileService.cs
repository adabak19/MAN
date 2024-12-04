using System.Collections.Generic;
using System.Linq;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Api.Services;

public class ProfileService : IProfileService{
    
    public async Task<List<Profile>> GetAllAsync(){
        using ApplicationDbContext context = new();
        return await context.Profiles.ToListAsync();
    }

    public async Task<Profile?> GetAsyncById(int id){
        using ApplicationDbContext context = new();
        return await context.Profiles.FindAsync(id);
    }
    public async Task<Profile> AddAsync(Profile profile){
        using ApplicationDbContext context = new();
        EntityEntry<Profile> entry = await context.Profiles.AddAsync(profile);
        await context.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task DeleteAsync(int id){
        using ApplicationDbContext context = new();
        var profile = await context.Profiles.FindAsync(id);
        if (profile is null){
            return;
        }
        context.Profiles.Remove(profile);
        await context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Profile profile){
        using ApplicationDbContext context = new();
        context.Profiles.Update(profile);
        await context.SaveChangesAsync();
    }

    public async Task<Profile?> GetAsyncByUsername(string username){
        using ApplicationDbContext context = new();
        return await context.Profiles.Where(x => x.ProfileName == username).FirstOrDefaultAsync();
    }
}
