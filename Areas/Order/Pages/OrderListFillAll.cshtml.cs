using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListFillAllModel : PageModel
{
    private readonly ILogger<OrderListFillAllModel> _logger;

    public OrderListFillAllModel(ILogger<OrderListFillAllModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
