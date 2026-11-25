using ShelfTrack.Application.Services;
using ShelfTrack.Tests.Fakes;
using Xunit;

namespace ShelfTrack.Tests;

public class BookServiceTests
{
    [Fact]
    public async Task AddAsync_TitleAndAuthor_SavesBookForReader()
    {
        // Arrange
        var bookRepository = new FakeBookRepository();
        var bookService = new BookService(bookRepository);

        // Act
        bool result = await bookService.AddAsync(1, " Testboek ", "Test Auteur", 320, null, null);

        // Assert
        Assert.True(result);
        Assert.Single(bookRepository.Books);
        Assert.Equal("Testboek", bookRepository.Books[0].Title);
        Assert.Equal(1, bookRepository.Books[0].ReaderId);
    }
    
    [Fact]
    public async Task AddAsync_EmptyTitle_ReturnsFalse()
    {
        // Arrange
        var bookRepository = new FakeBookRepository();
        var bookService = new BookService(bookRepository);

        // Act
        bool result = await bookService.AddAsync(1, " ", "Test Auteur", 320, null, null);

        // Assert
        Assert.False(result);
        Assert.Empty(bookRepository.Books);
    }
}