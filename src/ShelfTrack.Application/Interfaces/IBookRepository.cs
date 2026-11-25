using ShelfTrack.Domain;

namespace ShelfTrack.Application.Interfaces;

public interface IBookRepository
{
    Task AddAsync(Book book);
    Task<List<Book>> GetByReaderAsync(int readerId);
}