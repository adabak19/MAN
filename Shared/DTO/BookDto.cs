namespace MAN.Shared.DTO;

public class BookDto
{
    public int Id {get; set;}
    public string? ISBN {get; set;}
    public string? Title { get; set; }
    public string? AuthorName { get; set; }
    public string? Publisher {get; set;}
    
    public int? PageCount { get; set; }

    public int? YearPublished { get; set; }
    public List<string>? Genres { get; set; }
    public List<string>? CoAuthors {get; set;}
    public int Amount {get; set;}
}