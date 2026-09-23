namespace ShelfTrack.Domain;

public class ReadingEntry
{
    public int Id { get; set; }
    public ReadingStatus Status { get; set; }
    public int CurrentPage { get; set; }
    public DateTime StartDate { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
} 