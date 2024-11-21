namespace MAN.Api.Models;
using System.ComponentModel.DataAnnotations;

public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(50)]
        public string? ProfileName { get; set; }
    }