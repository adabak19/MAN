using System.Collections.Generic;
using MAN.Models;

namespace MAN.Services
{
    public static class CoAuthorService
    {
        static List<CoAuthors> CoAuthorsList { get; }
        static int nextId = 4;

        static CoAuthorService()
        {
            CoAuthorsList = new List<CoAuthors>
            {
                new CoAuthors { BookId = 1, AuthorId = 1 },
                new CoAuthors { BookId = 2, AuthorId = 1 },
                new CoAuthors { BookId = 3, AuthorId = 1 },
                // Add more initial data as needed
            };
        }

        public static List<CoAuthors> GetAll() => CoAuthorsList;

        public static CoAuthors? Get(int bookId, int authorId) => 
            CoAuthorsList.FirstOrDefault(ca => ca.BookId == bookId && ca.AuthorId == authorId);

        public static void Add(CoAuthors coAuthor)
        {
            coAuthor.BookId = nextId++;
            CoAuthorsList.Add(coAuthor);
        }

        public static void Delete(int bookId, int authorId)
        {
            var coAuthor = Get(bookId, authorId);
            if (coAuthor is null)
                return;

            CoAuthorsList.Remove(coAuthor);
        }

        public static void Update(CoAuthors coAuthor)
        {
            var index = CoAuthorsList.FindIndex(ca => ca.BookId == coAuthor.BookId && ca.AuthorId == coAuthor.AuthorId);
            if (index == -1)
                return;

            CoAuthorsList[index] = coAuthor;
        }
    }
}