using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TADA.Pages;

public class GuestShoppingCartModel : PageModel
{
    private readonly ILogger<GuestShoppingCartModel> _logger;

    public GuestShoppingCartModel(ILogger<GuestShoppingCartModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
