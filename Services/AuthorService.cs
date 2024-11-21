using System.Collections.Specialized;
using MAN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Services;

public class AuthorService{

    public async Task<List<Author>> GetAllAsync(){
        using ApplicationDbContext context = new();
        return await context.Authors.ToListAsync();
    }

    public async Task<Author?> GetAsyncById(int id){
        using ApplicationDbContext context = new();
        Author? author = await context.Authors.FindAsync(id);
        return author;
    }
    public async Task<Author> Add(Author author){
        using ApplicationDbContext context = new();
        EntityEntry<Author> entry = await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task Delete(int id){
        using ApplicationDbContext context = new();
        var author = await context.Authors.FindAsync(id);
        if(author is null)
            return;
        context.Authors.Remove(author);
        await context.SaveChangesAsync();
    }
    public async Task Update(Author author){
        using ApplicationDbContext context = new();
        context.Authors.Update(author);
        await context.SaveChangesAsync();
    }
}