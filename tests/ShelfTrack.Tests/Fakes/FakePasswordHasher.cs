using ShelfTrack.Application.Interfaces;

namespace ShelfTrack.Tests.Fakes;

public class FakePasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return "hash-from-" + password;
    }

    public bool Verify(string password, string hash)
    {
        return hash == "hash-from-" + password;
    }
}