using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShelfTrack.Application.Services;

namespace ShelfTrack.Web.Pages;

[Authorize]
public class AddBookModel(BookService bookService) : PageModel
{
    [BindProperty]
    [Display(Name = "Titel")]
    [Required(ErrorMessage = "Vul een titel in.")]
    public string Title { get; set; } = string.Empty;

    [BindProperty]
    [Display(Name = "Auteur")]
    [Required(ErrorMessage = "Vul een auteur in.")]
    public string Author { get; set; } = string.Empty;

    [BindProperty]
    [Display(Name = "Aantal pagina's")]
    [Range(1, 10000, ErrorMessage = "Vul een aantal tussen 1 en 10000 in.")]
    public int? PageCount { get; set; }

    [BindProperty]
    [Display(Name = "Beschrijving")]
    public string? Description { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        int readerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        bool isAdded = await bookService.AddAsync(readerId, Title, Author, PageCount, null, Description);
        if (!isAdded)
        {
            ModelState.AddModelError(string.Empty, "Vul een titel en een auteur in.");
            return Page();
        }

        return RedirectToPage("/Library");
    }
}