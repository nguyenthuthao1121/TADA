using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TADA.Dto;
using TADA.Model;
using TADA.Service;

namespace TADA.Pages;

public class LoginModel : PageModel
{
    private readonly IAuthenticationService authenticationService;
    private readonly ICustomerService customerService;
    private readonly IAdminService adminService;

    [BindProperty]
    public string Email { get; set; }
    [BindProperty]
    public string Password { get; set; }

    public string Message;

    public LoginModel(IAuthenticationService authenticationService, ICustomerService customerService, IAdminService adminService)
    {
        this.authenticationService = authenticationService;
        this.customerService = customerService;
        this.adminService = adminService;
    }

    public void OnGet()
    {
        
    }

    public void OnGetLogout()
    {
        HttpContext.Session.Remove("Email");
    }
    public IActionResult OnPost()
    {
        var account = authenticationService.GetAccount(Email, Password);
        if (account != null)
        {
            HttpContext.Session.SetInt32("Id", account.Id);
            HttpContext.Session.SetString("Type", account.Type ? "Customer" : "Admin");
            if (account.Type)
            {
                HttpContext.Session.SetString("Name", customerService.GetNameByAccountId(account.Id));
                return RedirectToPage("/Index");
            }
            else
            {
                HttpContext.Session.SetString("Name", adminService.GetNameByAccountId(account.Id));
                return RedirectToPage("/Admin/HomePageAdmin");
            }
        }
        else
        {
            Message = "Tài khoản không tồn tại hoặc mật khẩu không đúng";
            return Page();
        }
    }
}
