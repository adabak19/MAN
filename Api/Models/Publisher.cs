namespace MAN.Api.Models;
using System.ComponentModel.DataAnnotations;

public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PublisherName { get; set; }
    }