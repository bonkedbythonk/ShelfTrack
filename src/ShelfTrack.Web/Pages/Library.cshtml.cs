using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ShelfTrack.Web.Pages;

[Authorize]
public class LibraryModel : PageModel
{
    public void OnGet()
    {
        
    }
}