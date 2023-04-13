using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderDetailAdminModel : PageModel
{
    private readonly ILogger<OrderDetailAdminModel> _logger;

    public OrderDetailAdminModel(ILogger<OrderDetailAdminModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
