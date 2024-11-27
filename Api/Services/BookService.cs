using System.Collections.Specialized;
using MAN.Shared.DTO;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MAN.Api.Services;

public class BookService : IBookService{
    private readonly ApplicationDbContext _context;

public BookService(ApplicationDbContext context)
{
    _context = context;
}

    public async Task<List<BookDto>> GetAllAsync(){
        using ApplicationDbContext context = new();
        return await context.Books.Select(b => new BookDto{
            Id = b.Id,
            ISBN = b.ISBN,
            Title = b.Title,
            AuthorName = b.Author.MiddleName == null ? $"{b.Author.FirstName} {b.Author.LastName}" : $"{b.Author.FirstName} {b.Author.MiddleName} {b.Author.LastName}",
            Publisher = b.Publisher.PublisherName,
            PageCount = b.PageCount,
            YearPublished = b.YearPublished,
            Genres = b.BookGenres.Select(bg => bg.Genre.GenreName).ToList(),
            CoAuthors = (List<string>)b.Coauthors.Select(ca => $"{ca.Author.FirstName} {ca.Author.LastName}"),
            Amount = b.Amount
        }).ToListAsync();
    }

    public async Task<BookDto?> GetAsyncById(int id){
        using ApplicationDbContext context = new();
        return await context.Books
        .Where(b => b.Id == id)
        .Select(b => new BookDto
        {
            Id = b.Id,
            ISBN = b.ISBN,
            Title = b.Title,
            AuthorName = b.Author.MiddleName == null ? $"{b.Author.FirstName} {b.Author.LastName}" : $"{b.Author.FirstName} {b.Author.MiddleName} {b.Author.LastName}",
            Publisher = b.Publisher.PublisherName,
            PageCount = b.PageCount,
            YearPublished = b.YearPublished,
            Genres = b.BookGenres.Select(bg => bg.Genre.GenreName).ToList(),
            CoAuthors = (List<string>)b.Coauthors.Select(ca => $"{ca.Author.FirstName} {ca.Author.LastName}"),
            Amount = b.Amount
        })
        .FirstOrDefaultAsync();
    }
    public async Task<Book> Add(Book book){
        EntityEntry<Book> entry = await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task Delete(int id){
        var book = await _context.Books.FindAsync(id);
        if(book is null)
            return;
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
    public async Task Update(Book book){
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

 // Add SearchBooksAsync
public async Task<List<BookDto>> SearchBooksAsync(string? title, string? author, string? genre)
{
    var query = _context.Books
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
    var result = await query.Select(b => new BookDto
    {
        Id = b.Id,
        ISBN = b.ISBN,
        Title = b.Title,
        AuthorName = b.Author.MiddleName == null ? $"{b.Author.FirstName} {b.Author.LastName}" : $"{b.Author.FirstName} {b.Author.MiddleName} {b.Author.LastName}",
        Publisher = b.Publisher.PublisherName,
        PageCount = b.PageCount,
        YearPublished = b.YearPublished,
        Genres = b.BookGenres.Select(bg => bg.Genre.GenreName).ToList(),
        CoAuthors = (List<string>)b.Coauthors.Select(ca => $"{ca.Author.FirstName} {ca.Author.LastName}"),
        Amount = b.Amount
    }).ToListAsync();

    return result.ToList(); // Return a simplified object list
}


}