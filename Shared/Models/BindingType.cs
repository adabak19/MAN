// using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MAN.Shared.Models;

public class BindingType
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string Type { get; set; }

    public ICollection<Book> Books { get; set; }
}