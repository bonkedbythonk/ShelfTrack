using ShelfTrack.Application.Interfaces;
using ShelfTrack.Domain;

namespace ShelfTrack.Tests.Fakes;

public class FakeBookRepository : IBookRepository
{
    public List<Book> Books { get; } = new();

    public Task AddAsync(Book book)
    {
        Books.Add(book);
        return Task.CompletedTask;
    }

    public Task<List<Book>> GetByReaderAsync(int readerId)
    {
        return Task.FromResult(Books.Where(b => b.ReaderId == readerId).ToList());
    }
}