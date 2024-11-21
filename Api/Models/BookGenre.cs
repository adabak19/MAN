namespace MAN.Api.Models;
using System.ComponentModel.DataAnnotations;

public class BookGenre{
    [Key]
    public int GenreId {get; set;}
    public Genre? Genre {get;set;}
    [Key]
    public int BookId {get;set;}
    public Book? Book {get; set;}
}