using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class EditBookModel : PageModel
{
    private readonly ILogger<EditBookModel> _logger;

    public EditBookModel(ILogger<EditBookModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
