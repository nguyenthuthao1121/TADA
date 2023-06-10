using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;
using TADA.Service;

namespace TADA.Pages;

public class ForgotPwdModel : PageModel
{
    private readonly IAccountService accountService;
    private readonly IEmailService emailService;
    public string Message { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    [RegularExpression(@"^[a-zA-Z][-_.a-zA-Z0-9]{5,29}@g(oogle)?mail\.com$", ErrorMessage = "Email này không phải là email hợp lệ!")]
    public string Email { get; set; }
    public ForgotPwdModel(IAccountService _accountService, IEmailService emailService)
    {
        this.accountService = _accountService;
        this.emailService = emailService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (accountService.CheckExistEmail(Email))
        {
            Random rnd = new Random();
            string Otp = rnd.Next(100000, 999999).ToString();
            DateTime OtpExpiry = DateTime.Now.AddMinutes(1);
            var receiver = Email;
            var subject = "Mã xác minh quên mật khẩu";
            await emailService.SendEmailAsync(receiver, subject, emailService.MessageEmailForActiveAccount(emailService.ConvertHtmlToString("Mail/ForgetPassword.html"), Email, Otp));
            string encryptedEmail = emailService.EncryptEmail(Email);
            HttpContext.Session.SetString("ResetPasswordEmail", encryptedEmail);
            HttpContext.Session.SetString("ResetPasswordOtp", Otp);
            HttpContext.Session.SetString("OtpExpiry", OtpExpiry.ToString());
            return RedirectToPage("./ForgotPwd2", new {email = encryptedEmail});
        }
        else
        {
            Message = "Tài khoản email không tồn tại!";
            return Page();
        }
    }

}
