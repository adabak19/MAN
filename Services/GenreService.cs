using System.Collections.Specialized;
using MAN.Models;

namespace MAN.Services;

public static class GenreService{
    static List<Genre> Genres {get;}
    static int nextId = 4;
    static GenreService(){
        Genres = new List<Genre>
        {
            new Genre { Id = 1, GenreName = "Horror"},
            new Genre { Id = 2, GenreName = "Comedy"},
            new Genre { Id = 3, GenreName = "Thriller"}
        };
    }

    public static List<Genre> GetAll() => Genres;

    public static Genre? Get(int id) => Genres.FirstOrDefault(a => a.Id == id);
    public static void Add(Genre genre){
        genre.Id = nextId++;
        Genres.Add(genre);
    }
    public static void Delete(int id){
        var genre = Get(id);
        if(genre is null)
            return;
        Genres.Remove(genre);
    }
    public static void Update(Genre genre){
        var index = Genres.FindIndex(a => a.Id == genre.Id);
        if(index == -1)
            return;
        Genres[index] = genre;
    }
}
