using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class ShoppingCartEmptyModel : PageModel
{
    private readonly ILogger<ShoppingCartEmptyModel> _logger;

    public ShoppingCartEmptyModel(ILogger<ShoppingCartEmptyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
