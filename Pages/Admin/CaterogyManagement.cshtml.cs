using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class CategoryManagementModel : PageModel
{
    private readonly ILogger<CategoryManagementModel> _logger;

    public CategoryManagementModel(ILogger<CategoryManagementModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
