using ShelfTrack.Application.Interfaces;
using ShelfTrack.Domain;

namespace ShelfTrack.Tests.Fakes;

public class FakeReaderRepository : IReaderRepository
{
    public List<Reader> Readers { get; } = new();
    
    public Task<bool> ReaderExistsAsync(string email)
    {
        return Task.FromResult(Readers.Any(r => r.Email == email));
    }
    
    public Task AddAsync(Reader reader)
    {
        Readers.Add(reader);
        return Task.CompletedTask;
    }
}