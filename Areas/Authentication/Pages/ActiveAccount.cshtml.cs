using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TADA.Service;
using TADA.Utilities;

namespace TADA.Pages;

public class ActiveAccountModel : PageModel
{
    private readonly IAccountService accountService;
    private readonly ICustomerService customerService;
    private readonly IAddressService addressService;
    private readonly IAuthenticationService authenticationService;
    private readonly ICartService cartService;
    private readonly IEmailService emailService;

    [BindProperty]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    public string Otp { get; set; }
    [BindProperty(SupportsGet = true, Name = "email")]
    public string EncryptedEmail { get; set; }

    public ActiveAccountModel(IAccountService accountService, ICustomerService customerService, IAddressService addressService, IAuthenticationService authenticationService, ICartService cartService, IEmailService emailService)
    {
        this.accountService = accountService;
        this.customerService = customerService;
        this.addressService = addressService;
        this.authenticationService = authenticationService;
        this.cartService = cartService;
        this.emailService = emailService;
    }

    public void OnGet()
    {
        TempData["Email"] = emailService.DecryptEmail(EncryptedEmail);
    }
    public IActionResult OnPost()
    {
        var Email = emailService.DecryptEmail(EncryptedEmail);
        TempData["Email"] = Email;
        string rightOtp = HttpContext.Session.GetString("Otp");
        DateTime otpExpiry = DateTime.Parse(HttpContext.Session.GetString("OtpExpiry"));
        if (!Otp.Equals(rightOtp))
        {
            TempData["WrongOtp"] = "Mã xác minh không chính xác";
            return Page();
        }
        if (DateTime.Now < otpExpiry)
        {
            // Dang nhap, chuyen toi trang index
            var hashPassword = HttpContext.Session.GetString("Password");
            HttpContext.Session.Remove("Password");
            accountService.AddNewAccount(Email, hashPassword, true);
            customerService.AddDefaultCustomer(Email);
            addressService.AddDefaultAddress();
            var customer = authenticationService.GetAccount(Email, hashPassword);
            var customerInformation = customerService.GetCustomerByAccountId(customer.Id);
            cartService.AddCart(customerInformation.CustomerId);
            HttpContext.Session.SetInt32("Id", customer.Id);
            HttpContext.Session.SetString("Type", "Customer");
            HttpContext.Session.SetString("Name", customerInformation.Name);
            return RedirectToPage("UserInformation", new { area = "PersonalManagement" });
        }
        else
        {
            TempData["FailedMessage"] = "Mã xác minh đã hết hiệu lực. Vui lòng thử lại sau!";
            return Page();
        }

    }
}
