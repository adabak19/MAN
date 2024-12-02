namespace MAN.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Book
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(15)]
        public string? ISBN { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Title { get; set; }

        public int? PageCount { get; set; }

        public int? YearPublished { get; set; }

        public int? BindingTypeId { get; set; }

        public int? PublisherId { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public int Amount { get; set; }
       
    public BindingType? BindingType { get; set; }
    public Publisher? Publisher { get; set; }
    public Author? Author { get; set; }
    public ICollection<BookGenre>? BookGenres { get; set; }
    public ICollection<BookRead>? BookReads { get; set; }
    public ICollection<CoAuthors>? Coauthors { get; set; }
    
    }