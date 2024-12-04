using MAN.Shared.Models;

namespace MAN.Shared.DTO;
public class BookReadDto
{
    public int BookId { get; set; }
    public string? BookTitle { get; set; }
    public string? AuthorName {get; set;}
    public int ProfileId { get; set; }
    public string? ReviewerName { get; set; }
    public int? Rating { get; set; }
    public string? Review { get; set; }
    public string? Status {get; set;}
    public DateOnly? DateStarted {get; set;}
    public DateOnly? DateFinished { get; set; }
    
}
