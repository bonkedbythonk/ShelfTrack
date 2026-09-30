using ShelfTrack.Application.Interfaces;
using ShelfTrack.Domain;

namespace ShelfTrack.Application.Services;

public class AccountService(IReaderRepository readerRepository, IPasswordHasher passwordHasher)
{
    public async Task<bool> RegisterAsync(string email, string password)
    {
        string normalizedEmail = email.Trim().ToLower();
        
        if (await readerRepository.ReaderExistsAsync(normalizedEmail))
        {
            return false;
        }
        
        var reader = new Reader
        {
            Email = normalizedEmail,
            PasswordHash = passwordHasher.Hash(password)
        };
        await readerRepository.AddAsync(reader);
        return true;
    }
}