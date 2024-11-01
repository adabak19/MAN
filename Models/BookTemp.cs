namespace MAN.Models;

public class BookTemp
{
    public int Id {get; set;}
    public long ISBN {get;set;}
    public string? Title {get; set;}
    public int PageCount {get; set;}
    public int YearPublished {get; set;}
    public int BindingId {get;set;}
    public int PublisherId {get; set;}
    public int AuthorId {get; set;}
}