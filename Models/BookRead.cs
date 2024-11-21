namespace MAN.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BookRead
    {
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        public Profile? Profile { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int? Rating { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateFinished { get; set; }

        [MaxLength(7)]
        public string? Status { get; set; }
    }