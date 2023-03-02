using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListFillDoneModel : PageModel
{
    private readonly ILogger<OrderListFillDoneModel> _logger;

    public OrderListFillDoneModel(ILogger<OrderListFillDoneModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
