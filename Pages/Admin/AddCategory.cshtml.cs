using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class AddCategoryModel : PageModel
{
    private readonly ILogger<AddCategoryModel> _logger;

    public AddCategoryModel(ILogger<AddCategoryModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
