using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class ShoppingCartFillModel : PageModel
{
    private readonly ILogger<ShoppingCartFillModel> _logger;

    public ShoppingCartFillModel(ILogger<ShoppingCartFillModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
