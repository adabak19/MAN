using System.Collections.Specialized;
using MAN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Services;

public class BookService{

    public async Task<List<Book>> GetAllAsync(){
        using ApplicationDbContext context = new();
        return await context.Books.ToListAsync();
    }

    public async Task<Book?> GetAsyncById(int id){
        using ApplicationDbContext context = new();
        Book? book = await context.Books.FindAsync(id);
        return book;
    }
    public async Task<Book> Add(Book book){
        using ApplicationDbContext context = new();
        EntityEntry<Book> entry = await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task Delete(int id){
        using ApplicationDbContext context = new();
        var book = await context.Books.FindAsync(id);
        if(book is null)
            return;
        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }
    public async Task Update(Book book){
        using ApplicationDbContext context = new();
        context.Books.Update(book);
        await context.SaveChangesAsync();
    }
}