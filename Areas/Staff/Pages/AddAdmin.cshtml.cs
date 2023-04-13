using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class AddAdminModel : PageModel
{
    private readonly ILogger<AddAdminModel> _logger;

    public AddAdminModel(ILogger<AddAdminModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
