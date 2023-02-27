using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListEmptyAllModel : PageModel
{
    private readonly ILogger<OrderListEmptyAllModel> _logger;

    public OrderListEmptyAllModel(ILogger<OrderListEmptyAllModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
