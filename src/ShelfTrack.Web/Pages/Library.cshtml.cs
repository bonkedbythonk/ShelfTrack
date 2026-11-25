using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShelfTrack.Application.Services;
using ShelfTrack.Domain;

namespace ShelfTrack.Web.Pages;

[Authorize]
public class LibraryModel(BookService bookService) : PageModel
{
    public List<Book> Books { get; set; } = new();

    public async Task OnGetAsync()
    {
        int readerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        Books = await bookService.GetLibraryAsync(readerId);
    }
}