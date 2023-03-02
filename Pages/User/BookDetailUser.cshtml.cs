using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class BookDetailUserModel : PageModel
{
    private readonly ILogger<BookDetailUserModel> _logger;

    public BookDetailUserModel(ILogger<BookDetailUserModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
