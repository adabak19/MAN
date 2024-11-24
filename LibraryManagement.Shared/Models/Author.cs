namespace LibraryManagement.Shared.Models;
using System.ComponentModel.DataAnnotations;

 public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(250)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }
    }