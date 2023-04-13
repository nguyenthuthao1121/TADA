using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class ConfirmPackageModel : PageModel
{
    private readonly ILogger<ConfirmPackageModel> _logger;

    public ConfirmPackageModel(ILogger<ConfirmPackageModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
