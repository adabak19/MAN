using System.Collections.Specialized;
using MAN.Shared.DTO;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Api.Services;

public class AuthorService : IAuthorService
{

    public async Task<List<AuthorDto>> GetAllAsync()
    {
        using ApplicationDbContext context = new();
        return await context.Authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            AuthorName = a.MiddleName == null ? $"{a.FirstName} {a.LastName}" : $"{a.FirstName} {a.MiddleName} {a.LastName}",
            Books = (List<string>)a.Books.Select(b => b.Title)
        }).ToListAsync();
    }
    public async Task<List<Author>> GetAllAuthors(){
        using ApplicationDbContext context = new();
        return await context.Authors.ToListAsync();
    }

    public async Task<AuthorDto?> GetAsyncById(int id)
    {
        using ApplicationDbContext context = new();
        return await context.Authors
        .Where(a => a.Id == id)
        .Select(a => new AuthorDto
        {
            Id = a.Id,
            AuthorName = a.MiddleName == null ? $"{a.FirstName} {a.LastName}" : $"{a.FirstName} {a.MiddleName} {a.LastName}",
            Books = (List<string>)a.Books.Select(b => b.Title)
        })
        .FirstOrDefaultAsync();
    }
    public async Task<Author> Add(Author author)
    {
        using ApplicationDbContext context = new();
        EntityEntry<Author> entry = await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
        return entry.Entity;
    }
    public async Task Delete(int id)
    {
        using ApplicationDbContext context = new();
        var author = await context.Authors.FindAsync(id);
        if (author is null)
            return;
        context.Authors.Remove(author);
        await context.SaveChangesAsync();
    }
    public async Task Update(Author author)
    {
        using ApplicationDbContext context = new();
        context.Authors.Update(author);
        await context.SaveChangesAsync();
    }
    public async Task<List<AuthorDto>> SearchAuthorsAsync(string? name)
    {
        using ApplicationDbContext context = new();
        var query = context.Authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            AuthorName = a.FirstName,
            Books = (List<string>)a.Books.Select(b => b.Title)
        }).AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(a => EF.Functions.Like(a.AuthorName.ToLower(), $"%{name.ToLower()}%"));
        }

        // Flatten the results
        var result = await query.Select(a => new AuthorDto
        {
            Id = a.Id,
            AuthorName = a.AuthorName,
            Books = a.Books,
        }).ToListAsync();

        return result; // Return a simplified object list
    }
}