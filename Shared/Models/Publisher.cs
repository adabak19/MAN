namespace MAN.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? PublisherName { get; set; }
    }