using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class ForgotPwd2Model : PageModel
{
    private readonly ILogger<ForgotPwd2Model> _logger;

    public ForgotPwd2Model(ILogger<ForgotPwd2Model> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
