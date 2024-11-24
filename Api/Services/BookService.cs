using System.Collections.Specialized;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Api.Services;

public class BookService : IBookService{

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

 // Add SearchBooksAsync
public async Task<List<object>> SearchBooksAsync(string? title, string? author, string? genre)
{
    using ApplicationDbContext context = new();
    var query = context.Books
        .Include(b => b.Author) // Include only necessary related data
        .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
        .AsQueryable();

    if (!string.IsNullOrWhiteSpace(title))
    {
        query = query.Where(b => EF.Functions.Like(b.Title.ToLower(), $"%{title.ToLower()}%"));
    }

    if (!string.IsNullOrWhiteSpace(author))
    {
        query = query.Where(b =>
            EF.Functions.Like(b.Author.FirstName.ToLower(), $"%{author.ToLower()}%") ||
            EF.Functions.Like(b.Author.LastName.ToLower(), $"%{author.ToLower()}%"));
    }

    if (!string.IsNullOrWhiteSpace(genre))
    {
        query = query.Where(b => b.BookGenres.Any(bg => 
            EF.Functions.Like(bg.Genre.GenreName.ToLower(), $"%{genre.ToLower()}%")));
    }

    // Flatten the results
    var result = await query.Select(b => new
    {
        b.Id,
        b.Title,
        b.ISBN,
        AuthorName = b.Author != null ? $"{b.Author.FirstName} {b.Author.LastName}" : null,
        Genres = b.BookGenres.Select(bg => bg.Genre.GenreName).ToList()
    }).ToListAsync();

    return result.Cast<object>().ToList(); // Return a simplified object list
}


}