namespace MAN.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string GenreName { get; set; }

         // Navigation property for many-to-many relationship
    public ICollection<BookGenre>? BookGenres { get; set; }
    }