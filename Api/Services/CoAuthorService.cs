using System.Collections.Generic;
using MAN.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Api.Services
{
    public class CoAuthorService
    {

        public async Task<List<CoAuthors>> GetAllAsync(){
            using ApplicationDbContext context = new();
            return await context.CoAuthors.ToListAsync();
        }

        public async Task<CoAuthors?> GetAsyncById(int bookId, int authorId){
            using ApplicationDbContext context = new();
            CoAuthors? coAuthor = await context.CoAuthors.FirstOrDefaultAsync(ca => ca.BookId == bookId && ca.AuthorId == authorId);
            return coAuthor;
        }

        public async Task<CoAuthors> Add(CoAuthors coAuthor)
        {
            using ApplicationDbContext context = new();
            EntityEntry<CoAuthors> entry = await context.CoAuthors.AddAsync(coAuthor);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task Delete(int bookId, int authorId)
        {
            using ApplicationDbContext context = new();
            var coAuthor = await context.CoAuthors.FirstOrDefaultAsync(ca => ca.BookId == bookId && ca.AuthorId == authorId);
            if (coAuthor is null)
                return;
            context.CoAuthors.Remove(coAuthor);
            await context.SaveChangesAsync();
        }

        public async Task Update(CoAuthors coAuthor)
        {
            using ApplicationDbContext context = new();
            context.CoAuthors.Update(coAuthor);
            await context.SaveChangesAsync();
        }
    }
}