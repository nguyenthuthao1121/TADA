using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListFillReviewModel : PageModel
{
    private readonly ILogger<OrderListFillReviewModel> _logger;

    public OrderListFillReviewModel(ILogger<OrderListFillReviewModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
