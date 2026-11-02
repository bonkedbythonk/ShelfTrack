using ShelfTrack.Application.Services;
using ShelfTrack.Tests.Fakes;
using ShelfTrack.Domain;
using Xunit;

namespace ShelfTrack.Tests;

public class AccountServiceTests
{
    [Fact]
    public async Task RegisterAsync_NewEmail_SavesReaderWithHashedPassword()
    {
        // Arrange
        var readerRepository = new FakeReaderRepository();
        var passwordHasher = new FakePasswordHasher();
        var accountService = new AccountService(readerRepository, passwordHasher);
        
        // Act
        bool result = await accountService.RegisterAsync("Test@Example.com ", "password123");
        
        // Assert
        Assert.True(result);
        Assert.Single(readerRepository.Readers);
        Assert.Equal("test@example.com", readerRepository.Readers[0].Email);
        Assert.Equal("hash-from-password123", readerRepository.Readers[0].PasswordHash);
    }

    [Fact]
    public async Task RegisterAsync_ExistingEmail_ReturnsFalse()
    {
        //Arrange
        var passwordHasher = new FakePasswordHasher();
        var readerRepository = new FakeReaderRepository();
        var accountService = new AccountService(readerRepository, passwordHasher);
        
        readerRepository.ReaderExists = true;

        //Act
        bool result = await accountService.RegisterAsync("test@example.com", "password123");

        //Assert
        Assert.False(result);
    }
}