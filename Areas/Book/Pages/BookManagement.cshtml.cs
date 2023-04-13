using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class BookManagementModel : PageModel
{
    private readonly ILogger<BookManagementModel> _logger;

    public BookManagementModel(ILogger<BookManagementModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
