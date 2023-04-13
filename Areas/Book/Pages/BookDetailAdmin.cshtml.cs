using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class BookDetailAdminModel : PageModel
{
    private readonly ILogger<BookDetailAdminModel> _logger;

    public BookDetailAdminModel(ILogger<BookDetailAdminModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
