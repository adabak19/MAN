using System.Collections.Specialized;
using MAN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Services;

public class BookGenreService{

    public async Task<List<BookGenre>> GetAllAsync(){
        using ApplicationDbContext context = new();
        return await context.BookGenres.ToListAsync();
    }

    public async Task<BookGenre?> GetAsyncById(int bookId, int genreId){
        using ApplicationDbContext context = new();
        BookGenre? bookGenre = await context.BookGenres.FirstOrDefaultAsync(bg => bg.BookId == bookId && bg.GenreId == genreId);
        return bookGenre;
    }
    public async Task<BookGenre> Add(BookGenre bookGenre){
        using ApplicationDbContext context = new();
        EntityEntry<BookGenre> entry = await context.BookGenres.AddAsync(bookGenre);
        await context.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task Delete(int bookId, int genreId){
        using ApplicationDbContext context = new();
        var bookGenre = await context.BookGenres.FirstOrDefaultAsync(bg => bg.BookId == bookId && bg.GenreId == genreId);
        if(bookGenre is null)
            return;
        context.BookGenres.Remove(bookGenre);
        await context.SaveChangesAsync();
    }
    public async Task Update(BookGenre bookGenre){
        using ApplicationDbContext context = new();
        context.BookGenres.Update(bookGenre);
        await context.SaveChangesAsync();
    }
}