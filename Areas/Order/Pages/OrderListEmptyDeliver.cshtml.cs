using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListEmptyDeliverModel : PageModel
{
    private readonly ILogger<OrderListEmptyDeliverModel> _logger;

    public OrderListEmptyDeliverModel(ILogger<OrderListEmptyDeliverModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
