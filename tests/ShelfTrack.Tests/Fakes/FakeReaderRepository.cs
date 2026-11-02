using ShelfTrack.Application.Interfaces;
using ShelfTrack.Domain;

namespace ShelfTrack.Tests.Fakes;

public class FakeReaderRepository : IReaderRepository
{
    public List<Reader> Readers { get; } = new();
    
    public bool ReaderExists {get ; set; }
    
    public Task<bool> ReaderExistsAsync(string email)
    {
        return Task.FromResult(ReaderExists);
    }
    
    public Task AddAsync(Reader reader)
    {
        Readers.Add(reader);
        return Task.CompletedTask;
    }
}