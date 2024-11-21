// using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace MAN.Models;

public class BindingType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Type { get; set; }
    }