using System.Collections.Specialized;
using MAN.Models;

namespace MAN.Services;

public static class AuthorService{
    static List<Author> Authors {get;}
    static int nextId = 4;
    static AuthorService(){
        Authors = new List<Author>
        {
            new Author { Id = 1, FirstName = "John", MiddleName = "Ronald Reuel", LastName = "Tolkien"},
            new Author { Id = 2, FirstName = "dddd", MiddleName = "eeeee", LastName = "fffff"},
            new Author { Id = 3, FirstName = "ggggg", MiddleName = "hhhh", LastName = "jjjj" },
        };
    }

    public static List<Author> GetAll() => Authors;

    public static Author? Get(int id) => Authors.FirstOrDefault(a => a.Id == id);
    public static void Add(Author author){
        author.Id = nextId++;
        Authors.Add(author);
    }
    public static void Delete(int id){
        var author = Get(id);
        if(author is null)
            return;
        Authors.Remove(author);
    }
    public static void Update(Author author){
        var index = Authors.FindIndex(a => a.Id == author.Id);
        if(index == -1)
            return;
        Authors[index] = author;
    }
}
