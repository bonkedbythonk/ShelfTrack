using Microsoft.EntityFrameworkCore;
using ShelfTrack.Application.Interfaces;
using ShelfTrack.Domain;

namespace ShelfTrack.Persistence.Repositories;

public class ReaderRepository(ShelfTrackDbContext context) : IReaderRepository
{
    public async Task<bool> ReaderExistsAsync(string email)
    {
        return await context.Readers.AnyAsync(r => r.Email == email);
    }

    public async Task AddAsync(Reader reader)
    {
        context.Readers.Add(reader);
        await context.SaveChangesAsync();
    }
}