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
    
    [Fact]
    public async Task LoginAsync_CorrectCredentials_ReturnsReader()
    {
        // Arrange
        var passwordHasher = new FakePasswordHasher();
        var readerRepository = new FakeReaderRepository();
        var accountService = new AccountService(readerRepository, passwordHasher);

        var reader = new Reader
        {
            Email = "test@example.com",
            PasswordHash = "hash-from-password123"
        };
        readerRepository.Readers.Add(reader);

        // Act
        var result = await accountService.LoginAsync("test@example.com", "password123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test@example.com", result.Email);
    }
    
    [Fact]
    public async Task LoginAsync_IncorrectPassword_ReturnsNull()
    {
        // Arrange
        var passwordHasher = new FakePasswordHasher();
        var readerRepository = new FakeReaderRepository();
        var accountService = new AccountService(readerRepository, passwordHasher);
        
        var reader = new Reader
        {
            Email = "test@example.com",
            PasswordHash = "hash-from-password123"
        };
        readerRepository.Readers.Add(reader);
        
        // Act
        var result = await accountService.LoginAsync("test@example.com", "wrongpassword");
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task LoginAsync_NonExistentEmail_ReturnsNull()
    {
        // Arrange
        var passwordHasher = new FakePasswordHasher();
        var readerRepository = new FakeReaderRepository();
        var accountService = new AccountService(readerRepository, passwordHasher);
        
        // Act
        var result = await accountService.LoginAsync("nonexistent@example.com", "password123");
        
        // Assert
        Assert.Null(result);
    }
}