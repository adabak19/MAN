namespace MAN.Models;

public class BookRead
{
    public int ProfileId {get; set;}
    public int BookId {get;set;}
    public int Rating {get; set;}
    public date DateStarted {get; set;}
    public date DateFinished {get; set;}
    public bool Status {get; set;}


}