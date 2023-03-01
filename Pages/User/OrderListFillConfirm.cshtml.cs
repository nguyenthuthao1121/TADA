using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListFillConfirmModel : PageModel
{
    private readonly ILogger<OrderListFillConfirmModel> _logger;

    public OrderListFillConfirmModel(ILogger<OrderListFillConfirmModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
