namespace Domain.Entities;

public class Books
{
    public int BookId { get; set; }
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public int Publicationyear { get; set; }
    public int TotalCopies { get; set; }
    public int Availabcopies { get; set; }

}
