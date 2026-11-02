using Microsoft.EntityFrameworkCore;
using ShelfTrack.Domain;

namespace ShelfTrack.Persistence;

public class ShelfTrackDbContext(DbContextOptions<ShelfTrackDbContext> options) : DbContext(options)
{
    public DbSet<Reader> Readers { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<ReadingProgress> ReadingProgress { get; set; } = null!;
    public DbSet<Shelf> Shelves { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reader>()
            .HasIndex(r => r.Email)
            .IsUnique();
        
        modelBuilder.Entity<Shelf>()
            .HasIndex(s => new { s.ReaderId, s.Name })
            .IsUnique();
    }
}
