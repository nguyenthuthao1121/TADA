using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class ShoppingCardFillModel : PageModel
{
    private readonly ILogger<ShoppingCardFillModel> _logger;

    public ShoppingCardFillModel(ILogger<ShoppingCardFillModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
