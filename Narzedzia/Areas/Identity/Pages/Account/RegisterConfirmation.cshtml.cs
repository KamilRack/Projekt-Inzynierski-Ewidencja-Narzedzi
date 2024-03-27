using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class RegisterConfirmationModel : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }
}