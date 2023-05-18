using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.WebSockets;
using TADA.Service;
using TADA.Utilities;

namespace TADA.Pages;

public class SignupModel : PageModel
{
    private readonly IAuthenticationService authenticationService;
    private readonly ICustomerService customerService;
    private readonly IAccountService accountService;
    private readonly IAddressService addressService;
    private readonly ICartService cartService;

    [BindProperty]
    public string Email { get; set; }
    [BindProperty]
    public string Password { get; set; }
    [BindProperty]
    public string ConfirmPassword { get; set; }

    public string Message;
    public SignupModel(IAuthenticationService authenticationService, ICustomerService customerService, IAccountService accountService, IAddressService addressService, ICartService cartService)
    {
        this.authenticationService = authenticationService;
        this.customerService = customerService;
        this.accountService = accountService;
        this.addressService = addressService;
        this.cartService = cartService;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        if (Password.Equals(ConfirmPassword))
        {
            var hashPassword = HashPassword.Hash(Password);
            var account = authenticationService.GetAccount(Email, hashPassword);
            if (account != null)
            {
                Message = "Email đã được sử dụng để đăng ký tài khoản. Vui lòng sử dụng email khác để đăng ký";
                return Page();
            }
            else
            {
                accountService.AddNewAccount(Email, hashPassword, true);
                customerService.AddDefaultCustomer(Email);
                addressService.AddDefaultAddress();
                var customer = authenticationService.GetAccount(Email, hashPassword);
                var customerInformation = customerService.GetCustomerByAccountId(customer.Id);
                cartService.AddCart(customerInformation.CustomerId);
                HttpContext.Session.SetInt32("Id", customer.Id);
                HttpContext.Session.SetString("Type", "Customer");
                HttpContext.Session.SetString("Name", customerInformation.Name);
                //return RedirectToPage("/Authentication/UserInformation");
                return RedirectToPage("UserInformation", new { area = "PersonalManagement"});
                //return Page();
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
