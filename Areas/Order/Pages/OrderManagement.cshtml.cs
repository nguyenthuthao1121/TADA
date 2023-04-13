using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderManagementModel : PageModel
{
    private readonly ILogger<OrderManagementModel> _logger;

    public OrderManagementModel(ILogger<OrderManagementModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
