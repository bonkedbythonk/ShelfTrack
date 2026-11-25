using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShelfTrack.Application.Services;
using ShelfTrack.Web.Security;

namespace ShelfTrack.Web.Pages;

public class RegisterModel(AccountService accountService) : PageModel
{
    [BindProperty]
    [Display(Name = "E-mailadres")]
    [Required(ErrorMessage = "Vul een e-mailadres in.")]
    [EmailAddress(ErrorMessage = "Dit is geen geldig e-mailadres.")]
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
        var isRegistered = await accountService.RegisterAsync(Email, Password);
        if (!isRegistered)
        {
            ModelState.AddModelError(nameof(Email), "Dit e-mailadres is al in gebruik.");
            return Page();
        }
        var reader = await accountService.LoginAsync(Email, Password);

        await ReaderSignIn.SignInAsync(HttpContext, reader!);
        
        return RedirectToPage("/Library");
    }
}