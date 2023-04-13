using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListEmptyConfirmModel : PageModel
{
    private readonly ILogger<OrderListEmptyConfirmModel> _logger;

    public OrderListEmptyConfirmModel(ILogger<OrderListEmptyConfirmModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
