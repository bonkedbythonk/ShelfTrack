using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShelfTrack.Domain;

namespace ShelfTrack.Web.Security;

public static class ReaderSignIn
{
    public static async Task SignInAsync(HttpContext httpContext, Reader reader)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, reader.Id.ToString()),
            new(ClaimTypes.Name, reader.Email)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
    }
}