using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Text;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;
using TADA.Utilities;

namespace TADA.Pages;

public class SignupModel : PageModel
{
    private readonly IAuthenticationService authenticationService;
    private readonly IEmailService emailService;

    [BindProperty]
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Email này không phải là email hợp lệ!")]
    [RegularExpression(@"^[a-zA-Z][-_.a-zA-Z0-9]{5,29}@g(oogle)?mail\.com$", ErrorMessage = "Email này không phải là email hợp lệ!")]
    public string Email { get; set; }
    [BindProperty]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài tối thiểu là 6 ký tự!")]
    public string Password { get; set; }
    [BindProperty]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng!")]
    public string ConfirmPassword { get; set; }

    public string Message;
    public SignupModel(IAuthenticationService authenticationService, IEmailService emailService)
    {
        this.authenticationService = authenticationService;
        this.emailService = emailService;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {
        if (Password.Equals(ConfirmPassword))
        {
            var account = authenticationService.GetAccountByEmail(Email);
            if (account != null)
            {
                Message = "Email đã được sử dụng để đăng ký tài khoản. Vui lòng sử dụng email khác để đăng ký!";
                return Page();
            }
            else
            {
                try
                {
                    var hashPassword = HashPassword.Hash(Password);
                    Random rnd = new Random();
                    string Otp = rnd.Next(100000, 999999).ToString();
                    DateTime OtpExpiry = DateTime.Now.AddMinutes(1);
                    var receiver = Email;
                    var subject = "Mã kích hoạt tài khoản";
                    await emailService.SendEmailAsync(receiver, subject, emailService.MessageEmailForActiveAccount(emailService.ConvertHtmlToString("Mail/ActiveAccount.html"), Email, Otp));
                    string encryptedEmail = emailService.EncryptEmail(Email);
                    HttpContext.Session.SetString("Password", hashPassword);
                    HttpContext.Session.SetString("Otp", Otp);
                    HttpContext.Session.SetString("OtpExpiry", OtpExpiry.ToString());
                    return RedirectToPage("ActiveAccount", new { area = "Authentication", email = encryptedEmail });
                }
                catch (FormatException)
                {
                    Message = "Email này không phải là email hợp lệ!";
                    return Page();
                }
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
