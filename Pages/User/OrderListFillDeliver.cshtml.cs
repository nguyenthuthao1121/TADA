using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListFillDeliverModel : PageModel
{
    private readonly ILogger<OrderListFillDeliverModel> _logger;

    public OrderListFillDeliverModel(ILogger<OrderListFillDeliverModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
