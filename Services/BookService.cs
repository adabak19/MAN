using System.Collections.Specialized;
using MAN.Models;

namespace MAN.Services;

public static class BookService{
    static List<Book> Books {get;}
    static int nextId = 4;
    static BookService(){
        Books = new List<Book>
        {
            new Book { Id = 1, ISBN = 123, Title = "The Lord of the Ring: The Fellowship of the Ring", Author = "J.R.R. Tolkien", YearPublished = 1954 },
            new Book { Id = 2, ISBN = 124, Title = "The Lord of the Ring: The Two Towers", Author = "J.R.R. Tolkien", YearPublished = 1954 },
            new Book { Id = 3, ISBN = 125, Title = "The Lord of the Ring: The Return of the King", Author = "J.R.R. Tolkien", YearPublished = 1954 },
        };
    }

    public static List<Book> GetAll() => Books;

    public static Book? Get(int id) => Books.FirstOrDefault(b => b.Id == id);
    public static void Add(Book book){
        book.Id = nextId++;
        Books.Add(book);
    }
    public static void Delete(int id){
        var book = Get(id);
        if(book is null)
            return;
        Books.Remove(book);
    }
    public static void Update(Book book){
        var index = Books.FindIndex(b => b.Id == book.Id);
        if(index == -1)
            return;
        Books[index] = book;
    }
}
