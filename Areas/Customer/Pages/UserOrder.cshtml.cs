using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class UserOrderModel : PageModel
{
    private readonly ILogger<UserOrderModel> _logger;

    public UserOrderModel(ILogger<UserOrderModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
