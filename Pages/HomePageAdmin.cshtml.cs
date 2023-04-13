using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class HomePageAdminModel : PageModel
{
    private readonly ILogger<HomePageAdminModel> _logger;

    public HomePageAdminModel(ILogger<HomePageAdminModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
