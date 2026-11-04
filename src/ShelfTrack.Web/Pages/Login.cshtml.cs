using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShelfTrack.Application.Services;

namespace ShelfTrack.Web.Pages;

public class LoginModel(AccountService accountService) : PageModel
{
    [BindProperty]
    [Display(Name = "E-mailadres")]
    [Required(ErrorMessage = "Vul een e-mailadres in.")]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Display(Name = "Wachtwoord")]
    [Required(ErrorMessage = "Vul een wachtwoord in.")]
    public string Password { get; set; } = string.Empty;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var reader = await accountService.LoginAsync(Email, Password);
        
        if (reader == null)
        {
            ModelState.AddModelError(string.Empty, "E-mailadres of wachtwoord is onjuist.");
            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, reader.Id.ToString()),
            new(ClaimTypes.Name, reader.Email)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        return RedirectToPage("/Index");
    }
    
}