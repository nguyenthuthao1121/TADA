using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListEmptyCancleModel : PageModel
{
    private readonly ILogger<OrderListEmptyCancleModel> _logger;

    public OrderListEmptyCancleModel(ILogger<OrderListEmptyCancleModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
