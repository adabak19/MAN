using MAN.Shared.Models;

namespace MAN.Shared.DTO;

public class AuthorDto
{

    public string FirstName {get; set;}
    public string? MiddleName {get; set;}
    public string LastName {get; set;}
    public List<string>? Books {get; set;}
}