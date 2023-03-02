using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListEmptyCancelModel : PageModel
{
    private readonly ILogger<OrderListEmptyCancelModel> _logger;

    public OrderListEmptyCancelModel(ILogger<OrderListEmptyCancelModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
