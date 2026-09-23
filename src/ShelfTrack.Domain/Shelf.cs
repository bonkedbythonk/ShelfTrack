namespace ShelfTrack.Domain;

public class Shelf
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public int ReaderId { get; set; }
    public Reader Reader { get; set; } = null!;
}