using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class EvaluateUserModel : PageModel
{
    private readonly ILogger<EvaluateUserModel> _logger;

    public EvaluateUserModel(ILogger<EvaluateUserModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
