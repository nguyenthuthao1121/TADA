using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListFillCancelModel : PageModel
{
    private readonly ILogger<OrderListFillCancelModel> _logger;

    public OrderListFillCancelModel(ILogger<OrderListFillCancelModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
