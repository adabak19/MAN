using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Shared.Models;
using LibraryManagement.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Api.Services;

public class GenreService : IGenreService{

    public async Task<List<Genre>> GetAllAsync(){
        using ApplicationDbContext context = new();
        return await context.Genres.ToListAsync();
    }
    public async Task<Genre?> GetAsyncById(int id){
        using ApplicationDbContext context = new();
        return await context.Genres.FindAsync(id);;
    }
    public async Task<Genre> Add(Genre genre){
        using ApplicationDbContext context = new();
        EntityEntry<Genre> entry = await context.Genres.AddAsync(genre);
        await context.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task Delete(int id){
        using ApplicationDbContext context = new();
        var genre = await context.Genres.FindAsync(id);
        if (genre is null){
            return;
        }
        context.Genres.Remove(genre);
        await context.SaveChangesAsync();
    }
    public async Task Update(Genre genre){
        using ApplicationDbContext context = new();
        context.Genres.Update(genre);
        await context.SaveChangesAsync();
    }
}
