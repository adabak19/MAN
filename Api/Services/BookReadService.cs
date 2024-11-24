using System.Collections.Generic;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Api.Services
{
    public class BookReadService : IBookReadService
    {

        public async Task<List<BookRead>> GetAllAsync(){
            using ApplicationDbContext context = new();
            return await context.BookReads.ToListAsync();
        }

        public async Task<BookRead?> GetAsyncById(int profileId, int bookId){
            using ApplicationDbContext context = new();
            BookRead? bookRead = await context.BookReads.FirstOrDefaultAsync(br => br.ProfileId == profileId && br.BookId == bookId);
            return bookRead;
        }

        public async Task<BookRead> Add(BookRead bookRead)
        {
            using ApplicationDbContext context = new();
            EntityEntry<BookRead> entry = await context.BookReads.AddAsync(bookRead);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task Delete(int profileId, int bookId)
        {
            using ApplicationDbContext context = new();
            var bookRead = await context.BookReads.FirstOrDefaultAsync(br => br.ProfileId == profileId && br.BookId == bookId);
            if (bookRead is null)
                return;
            context.BookReads.Remove(bookRead);
            await context.SaveChangesAsync();
        }

        public async Task Update(BookRead bookRead)
        {
            using ApplicationDbContext context = new();
            context.BookReads.Update(bookRead);
            await context.SaveChangesAsync();
        }
    }
}