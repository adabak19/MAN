using System.Collections.Generic;
using MAN.Models;

namespace MAN.Services
{
    public static class BookReadService
    {
        static List<BookRead> BookReads { get; }
        static int nextId = 4;

        static BookReadService()
        {
            BookReads = new List<BookRead>
            {
                new BookRead { ProfileId = 1, BookId = 1, Rating = 5, DateStarted = new DateTime(2023, 1, 1), DateFinished = new DateTime(2023, 1, 10), Status = true },
                new BookRead { ProfileId = 2, BookId = 2, Rating = 4, DateStarted = new DateTime(2023, 2, 1), DateFinished = new DateTime(2023, 2, 10), Status = true },
                new BookRead { ProfileId = 3, BookId = 3, Rating = 3, DateStarted = new DateTime(2023, 3, 1), DateFinished = new DateTime(2023, 3, 10), Status = true },
            };
        }

        public static List<BookRead> GetAll() => BookReads;

        public static BookRead? Get(int profileId, int bookId) =>
            BookReads.FirstOrDefault(br => br.ProfileId == profileId && br.BookId == bookId);

        public static void Add(BookRead bookRead)
        {
            BookReads.Add(bookRead);
        }

        public static void Delete(int profileId, int bookId)
        {
            var bookRead = Get(profileId, bookId);
            if (bookRead is null)
                return;

            BookReads.Remove(bookRead);
        }

        public static void Update(BookRead bookRead)
        {
            var index = BookReads.FindIndex(br => br.ProfileId == bookRead.ProfileId && br.BookId == bookRead.BookId);
            if (index == -1)
                return;

            BookReads[index] = bookRead;
        }
    }
}