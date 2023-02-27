using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class ShoppingCardEmptyModel : PageModel
{
    private readonly ILogger<ShoppingCardEmptyModel> _logger;

    public ShoppingCardEmptyModel(ILogger<ShoppingCardEmptyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
