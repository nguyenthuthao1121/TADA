using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListEmptyDoneModel : PageModel
{
    private readonly ILogger<OrderListEmptyDoneModel> _logger;

    public OrderListEmptyDoneModel(ILogger<OrderListEmptyDoneModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
