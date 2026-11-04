using Microsoft.AspNetCore.Identity;
using ShelfTrack.Application.Interfaces;
using ShelfTrack.Domain;

namespace ShelfTrack.Web.Security;

public class IdentityPasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<Reader> _hasher = new();

    public string Hash(string password)
    {
        return _hasher.HashPassword(new Reader(), password);
    }
    
    public bool Verify(string password, string hash)
    {
        var result = _hasher.VerifyHashedPassword(new Reader(), hash, password);
        return result != PasswordVerificationResult.Failed;    }
}