namespace MAN.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class CoAuthors
{
    [Key]
    [Column(Order = 0)]
    public int BookId {get; set;}
    [Key]
    [Column(Order = 1)]
    public int AuthorId {get; set;}
    [JsonIgnore]
    public Book Book { get; set; }
    [JsonIgnore]
    public Author Author { get; set; }   
}