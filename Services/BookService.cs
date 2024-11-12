using System.Collections.Specialized;
using MAN.Models;

namespace MAN.Services;

public static class BookService{
    static List<Bog> Books {get;}
    static int nextId = 4;
    static BookService(){
        Books = new List<Bog>
        {
            new Bog { Id = 1, ISBN = 123, Title = "The Lord of the Ring: The Fellowship of the Ring", AuthorId = 1, BindingId = 1, PageCount = 530, PublisherId = 1, YearPublished = 1954 },
            new Bog { Id = 2, ISBN = 124, Title = "The Lord of the Ring: The Two Towers", AuthorId = 1, BindingId = 1, PageCount = 530, PublisherId = 1, YearPublished = 1954 },
            new Bog { Id = 3, ISBN = 125, Title = "The Lord of the Ring: The Return of the King", AuthorId = 1, BindingId = 1, PageCount = 530, PublisherId = 1, YearPublished = 1954  },
        };
    }

    public static List<Bog> GetAll() => Books;

    public static Bog? Get(int id) => Books.FirstOrDefault(b => b.Id == id);
    public static void Add(Bog book){
        book.Id = nextId++;
        Books.Add(book);
    }
    public static void Delete(int id){
        var book = Get(id);
        if(book is null)
            return;
        Books.Remove(book);
    }
    public static void Update(Bog book){
        var index = Books.FindIndex(b => b.Id == book.Id);
        if(index == -1)
            return;
        Books[index] = book;
    }
}
