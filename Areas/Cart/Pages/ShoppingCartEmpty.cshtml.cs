using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Service;

namespace TADA.Pages;

public class ShoppingCartEmptyModel : PageModel
{
    private readonly IAccountService accountService;


    public string Username;
    public ShoppingCartEmptyModel(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    public void OnGet()
    {
        Username = HttpContext.Session.GetString("Name");

    }
}
