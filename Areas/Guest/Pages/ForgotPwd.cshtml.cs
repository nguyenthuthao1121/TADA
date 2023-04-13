using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class ForgotPwdModel : PageModel
{
    private readonly ILogger<ForgotPwdModel> _logger;

    public ForgotPwdModel(ILogger<ForgotPwdModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
