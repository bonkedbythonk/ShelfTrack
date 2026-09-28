namespace ShelfTrack.Domain;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int? PageCount { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? Description { get; set; }
    
    public int ReaderId { get; set; }
    public Reader Reader { get; set; } = null!;
    
    public ICollection<ReadingEntry> ReadingEntries { get; set; } = new List<ReadingEntry>();
    public ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
}