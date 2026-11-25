using Microsoft.EntityFrameworkCore;
using ShelfTrack.Application.Interfaces;
using ShelfTrack.Domain;

namespace ShelfTrack.Persistence.Repositories;

public class BookRepository(ShelfTrackDbContext context) : IBookRepository
{
    public async Task AddAsync(Book book)
    {
        context.Books.Add(book);
        await context.SaveChangesAsync();
    }

    public async Task<List<Book>> GetByReaderAsync(int readerId)
    {
        return await context.Books
            .Where(b => b.ReaderId == readerId)
            .OrderBy(b => b.Title)
            .ToListAsync();
    }
}