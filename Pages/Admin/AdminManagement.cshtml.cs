using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class AdminManagementModel : PageModel
{
    private readonly ILogger<AdminManagementModel> _logger;

    public AdminManagementModel(ILogger<AdminManagementModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
