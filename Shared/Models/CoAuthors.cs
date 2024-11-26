namespace MAN.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class CoAuthors
{
    [Key]
    [Column(Order = 0)]
    public int BookId {get; set;}
    [Key]
    [Column(Order = 1)]
    public int AuthorId {get; set;}
    public Book Book { get; set; }
    public Author Author { get; set; }   
}