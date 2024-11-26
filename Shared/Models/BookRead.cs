namespace MAN.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

public class BookRead
    {
        [Key]
        [Column(Order = 0)]
        public int ProfileId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int BookId { get; set; }

        public int? Rating { get; set; }
        public string? Review { get; set; }
        public DateOnly? DateStarted { get; set; }
        public DateOnly? DateFinished { get; set; }

        [MaxLength(7)]
        public string? Status { get; set; }
       
        public Profile? Profile { get; set; }
        public Book? Book { get; set; }
    }