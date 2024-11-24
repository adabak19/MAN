using System.Collections.Generic;
using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MAN.Api.Services
{
    public class BookRentedService : IBookRentedService
    {

        public async Task<List<BookRented>> GetAllAsync(){
            using ApplicationDbContext context = new();
            return await context.BookRented.ToListAsync();
        }

        public async Task<BookRented?> GetAsyncById(int profileId, int bookId){
            using ApplicationDbContext context = new();
            BookRented? bookRented = await context.BookRented.FirstOrDefaultAsync(br => br.ProfileId == profileId && br.BookId == bookId);
            return bookRented;
        }

        public async Task<BookRented> Add(BookRented bookRented)
        {
            using ApplicationDbContext context = new();
            EntityEntry<BookRented> entry = await context.BookRented.AddAsync(bookRented);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task Delete(int profileId, int bookId)
        {
            using ApplicationDbContext context = new();
            var bookRented = await context.BookRented.FirstOrDefaultAsync(br => br.ProfileId == profileId && br.BookId == bookId);
            if (bookRented is null)
                return;
            context.BookRented.Remove(bookRented);
            await context.SaveChangesAsync();
        }

        public async Task Update(BookRented bookRented)
        {
            using ApplicationDbContext context = new();
            context.BookRented.Update(bookRented);
            await context.SaveChangesAsync();
        }
    }
}