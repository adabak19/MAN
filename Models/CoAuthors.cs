namespace MAN.Models;
using System.ComponentModel.DataAnnotations;

public class CoAuthors
{
    [Key]
    public int BookId {get; set;}
    public Book? Book {get; set;}
    [Key]
    public int AuthorId {get; set;}
    public ICollection<Author>? Author {get; set;}    
}