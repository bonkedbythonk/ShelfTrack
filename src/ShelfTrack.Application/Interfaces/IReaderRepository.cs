using ShelfTrack.Domain;

namespace ShelfTrack.Application.Interfaces;

public interface IReaderRepository
{
    Task<bool> ReaderExistsAsync(string email);
    Task AddAsync(Reader reader);
    Task<Reader?> GetByEmailAsync(string email);
}