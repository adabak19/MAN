using MAN.Shared.Models;

namespace MAN.Shared.DTO;

public class AuthorDto
{
    public int Id {get; set;}
    public string AuthorName {get; set;}
    public List<string>? Books {get; set;}
}