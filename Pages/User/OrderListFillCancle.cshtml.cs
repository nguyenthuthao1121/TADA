using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListFillCancleModel : PageModel
{
    private readonly ILogger<OrderListFillCancleModel> _logger;

    public OrderListFillCancleModel(ILogger<OrderListFillCancleModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
