namespace MAN.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("BindingType")]
        public int? BindingTypeId { get; set; }
        public BindingType? BindingType { get; set; }

        [ForeignKey("Publisher")]
        public int? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    
     // Navigation property for many-to-many relationship
    public ICollection<BookGenre>? BookGenres { get; set; }
    }