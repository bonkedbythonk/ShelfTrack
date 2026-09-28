namespace ShelfTrack.Domain;

public class Reader
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
}