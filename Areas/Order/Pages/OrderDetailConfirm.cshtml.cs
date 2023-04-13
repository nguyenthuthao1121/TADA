using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderDetailConfirmModel : PageModel
{
    private readonly ILogger<OrderDetailConfirmModel> _logger;

    public OrderDetailConfirmModel(ILogger<OrderDetailConfirmModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
