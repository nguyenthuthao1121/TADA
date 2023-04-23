using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Service;

namespace TADA.Pages;

public class SignupModel : PageModel
{
    private readonly IAuthenticationService authenticationService;
    private readonly ICustomerService customerService;
    private readonly IAccountService accountService;

    [BindProperty]
    public string Email { get; set; }
    [BindProperty]
    public string Password { get; set; }
    [BindProperty]
    public string ConfirmPassword { get; set; }

    public string Message;
    public SignupModel(IAuthenticationService authenticationService, ICustomerService customerService, IAccountService accountService)
    {
        this.authenticationService = authenticationService;
        this.customerService = customerService;
        this.accountService = accountService;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        if (Password.Equals(ConfirmPassword))
        {
            var account = authenticationService.GetAccount(Email, Password);
            if (account != null)
            {
                Message = "Email đã được sử dụng để đăng ký tài khoản. Vui lòng sử dụng email khác để đăng ký";
                return Page();
            }
            else
            {
                accountService.AddNewAccount(Email, Password, true);
                //return RedirectToPage("/Authentication/UserInformation");
                //return RedirectToPage("UserInformation", new { area = "PersonalManagement"});
                return Page();
            }
        }
        else
        {
            // Cai ni vai bua lam validation
            Message = "Mật khẩu nhập lại không trùng khớp";
            return Page();
        }
    }
}
