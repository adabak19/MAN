using System.Collections.Specialized;
using MAN.Shared.DTO;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MAN.Api.Services;

public class BookService : IBookService{
    

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
    public async Task Update(BookDto bookDto){
        using ApplicationDbContext context = new();
        Book? book = await context.Books.Where(b => b.Id == bookDto.Id).FirstOrDefaultAsync();
        Book? newBook = new() {
            Id = bookDto.Id,
            ISBN = bookDto.ISBN,
            Title = bookDto.Title,
            PageCount = bookDto.PageCount,
            YearPublished = bookDto.YearPublished,
            BindingTypeId = book.BindingTypeId,
            PublisherId = book.PublisherId,
            AuthorId = book.AuthorId,
            Amount = bookDto.Amount
        }; 
        context.Books.Update(newBook);
        await context.SaveChangesAsync();
    }

 // Add SearchBooksAsync
public async Task<List<BookDto>> SearchBooksAsync(string? title, string? author, string? genre)
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

    return result; // Return a simplified object list
}

public async Task<List<BookDto>> SearchBooksForUserAsync(int profileId, string? title, string? author, string? genre)
{
    using var context = new ApplicationDbContext(); // If using dependency injection, use _context

    // Start with the user's books
    var userBooks = context.BookReads
        .Include(br => br.Book)
        .ThenInclude(b => b.Author)
        .Include(br => br.Book)
        .ThenInclude(b => b.BookGenres)
        .ThenInclude(bg => bg.Genre)
        .Where(br => br.ProfileId == profileId)
        .AsQueryable();

    // Apply filters
    if (!string.IsNullOrWhiteSpace(title))
    {
        userBooks = userBooks.Where(b => EF.Functions.Like(b.Book.Title.ToLower(), $"%{title.ToLower()}%"));
    }

    if (!string.IsNullOrWhiteSpace(author))
    {
        userBooks = userBooks.Where(b =>
            EF.Functions.Like(b.Book.Author.FirstName.ToLower(), $"%{author.ToLower()}%") ||
            EF.Functions.Like(b.Book.Author.LastName.ToLower(), $"%{author.ToLower()}%"));
    }

    if (!string.IsNullOrWhiteSpace(genre))
    {
        userBooks = userBooks.Where(b => b.Book.BookGenres.Any(bg =>
            EF.Functions.Like(bg.Genre.GenreName.ToLower(), $"%{genre.ToLower()}%")));
    }

    // Map results to DTOs
    var result = await userBooks.Select(b => new BookDto
    {
        Id = b.Book.Id,
        Title = b.Book.Title,
        AuthorName = $"{b.Book.Author.FirstName} {b.Book.Author.LastName}",
        Publisher = b.Book.Publisher.PublisherName,
        PageCount = b.Book.PageCount,
        YearPublished = b.Book.YearPublished,
        Genres = b.Book.BookGenres.Select(bg => bg.Genre.GenreName).ToList(),
        Amount = b.Book.Amount
    }).ToListAsync();

    return result;
}



}