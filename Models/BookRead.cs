namespace MAN.Models;
using System.ComponentModel.DataAnnotations;

public class BookRead
{
    [Key]
    public int ProfileId {get; set;}
    public Profile? Profile {get; set;}
    [Key]
    public int BookId {get;set;}
    public Book? Book {get; set;}
    public int Rating {get; set;}
    public DateOnly DateStarted {get; set;}
    public DateOnly DateFinished {get; set;}
    [MaxLength(7)]
    public string? Status {get; set;}
}