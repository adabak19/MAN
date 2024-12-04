using System.Collections.Generic;
using MAN.Shared.Models;
using MAN.Shared.DTO;
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
        
      public async Task<List<BookReadDto>> GetAsyncByBookId(int bookId)
        {   
            using ApplicationDbContext context = new();
            var bookReads = await context.BookReads
            .Include(br => br.Book)
            .Include(br => br.Profile)
            .Where(br => br.BookId == bookId)
            .ToListAsync();

            return bookReads.Select(br => new BookReadDto
                {
                    BookId = br.BookId,
                    BookTitle = br.Book?.Title,
                    AuthorName = br.Book?.Author?.MiddleName == null ? $"{br.Book?.Author?.FirstName} {br.Book?.Author?.LastName}" : $"{br.Book?.Author?.FirstName} {br.Book?.Author?.MiddleName} {br.Book?.Author?.LastName}",
                    ProfileId = br.ProfileId,
                    ReviewerName = br.Profile != null ? $"{br.Profile.FirstName} {br.Profile.LastName}" : null,
                    Rating = br.Rating,
                    Review = br.Review,
                    DateFinished = br.DateFinished,
                    Status = br.Status
                }).ToList();
        }
        public async Task<List<BookReadDto>> GetAsyncByProfileId(int profileId)
        {   
            using ApplicationDbContext context = new();
            var bookReads = await context.BookReads
            .Include(br => br.Book)
            .Include(br => br.Profile)
            .Where(br => br.ProfileId == profileId)
            .ToListAsync();

            return bookReads.Select(br => new BookReadDto
                {
                    BookId = br.BookId,
                    BookTitle = br.Book?.Title,
                    AuthorName = br.Book?.Author?.MiddleName == null ? $"{br.Book?.Author?.FirstName} {br.Book?.Author?.LastName}" : $"{br.Book?.Author?.FirstName} {br.Book?.Author?.MiddleName} {br.Book?.Author?.LastName}",
                    ProfileId = br.ProfileId,
                    ReviewerName = br.Profile != null ? $"{br.Profile.FirstName} {br.Profile.LastName}" : null,
                    Rating = br.Rating,
                    Review = br.Review,
                    DateStarted = br.DateStarted,
                    DateFinished = br.DateFinished,
                    Status = br.Status
                }).ToList();
        }
        public async Task<List<BookReadDto>> GetAllReading()
        {
            using ApplicationDbContext context = new();
            var bookReads = await context.BookReads
            .Include(br => br.Book)
            .Include(br => br.Profile)
            .Where(br => br.Status == "reading")
            .ToListAsync();

            return bookReads.Select(br => new BookReadDto
                {
                    BookId = br.BookId,
                    BookTitle = br.Book?.Title,
                    AuthorName = br.Book?.Author?.MiddleName == null ? $"{br.Book?.Author?.FirstName} {br.Book?.Author?.LastName}" : $"{br.Book?.Author?.FirstName} {br.Book?.Author?.MiddleName} {br.Book?.Author?.LastName}",
                    ProfileId = br.ProfileId,
                    ReviewerName = br.Profile != null ? $"{br.Profile.FirstName} {br.Profile.LastName}" : null,
                    Rating = br.Rating,
                    Review = br.Review,
                    DateStarted = br.DateStarted,
                    DateFinished = br.DateFinished,
                    Status = br.Status
                }).ToList();
        }

        

    }
}