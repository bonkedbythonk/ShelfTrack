using ShelfTrack.Application.Interfaces;
using ShelfTrack.Domain;

namespace ShelfTrack.Application.Services;

public class BookService(IBookRepository bookRepository)
{
    public async Task<bool> AddAsync(int readerId, string title, string author, int? pageCount, string? coverImageUrl, string? description)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
        {
            return false;
        }

        var book = new Book
        {
            ReaderId = readerId,
            Title = title.Trim(),
            Author = author.Trim(),
            PageCount = pageCount,
            CoverImageUrl = coverImageUrl,
            Description = description
        };

        await bookRepository.AddAsync(book);
        return true;
    }

    public async Task<List<Book>> GetLibraryAsync(int readerId)
    {
        return await bookRepository.GetByReaderAsync(readerId);
    }
}