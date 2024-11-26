namespace MAN.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string ProfileName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }

        public ICollection<BookRead>? BookReads { get; set; }
    }