using System.Collections.Generic;
using System.Linq;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Api.Services;
    public class PublisherService : IPublisherService
    {
    public async Task<List<Publisher>> GetAllAsync(){
        using ApplicationDbContext context = new();
        return await context.Publishers.ToListAsync();
    }

    public async Task<Publisher?> GetAsyncById(int id){
        using ApplicationDbContext context = new();
        return await context.Publishers.FindAsync(id);
    }
    public async Task<Publisher> AddAsync(Publisher publisher){
        using ApplicationDbContext context = new();
        EntityEntry<Publisher> entry = await context.Publishers.AddAsync(publisher);
        await context.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task DeleteAsync(int id){
        using ApplicationDbContext context = new();
        var publisher = await context.Publishers.FindAsync(id);
        if (publisher is null){
            return;
        }
        context.Publishers.Remove(publisher);
        await context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Publisher publisher){
        using ApplicationDbContext context = new();
        context.Publishers.Update(publisher);
        await context.SaveChangesAsync();
    }
}