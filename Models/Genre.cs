namespace MAN.Models;
using System.ComponentModel.DataAnnotations;

public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? GenreName { get; set; }
    }