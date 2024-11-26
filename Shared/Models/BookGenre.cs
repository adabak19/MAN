namespace MAN.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BookGenre{
    [Key]
    [Column(Order = 0)]
    public int GenreId {get; set;}
    [Key]
    [Column(Order = 1)]
    public int BookId {get;set;}

    public Genre? Genre {get;set;}
    public Book? Book {get; set;}
}