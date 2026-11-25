using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShelfTrack.Application.Services;
using ShelfTrack.Web.Security;

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

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
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

        await ReaderSignIn.SignInAsync(HttpContext, reader);
        
        if (Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }
        return RedirectToPage("/Library");
    }
    
}