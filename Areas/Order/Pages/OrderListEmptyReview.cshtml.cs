using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class OrderListEmptyReviewModel : PageModel
{
    private readonly ILogger<OrderListEmptyReviewModel> _logger;

    public OrderListEmptyReviewModel(ILogger<OrderListEmptyReviewModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
