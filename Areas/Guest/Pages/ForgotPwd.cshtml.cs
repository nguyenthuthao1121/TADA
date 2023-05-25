using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            var subject = "Mã xác minh mật khẩu";
            var message = "<h3>Đây là mã dùng một lần. Mã có hiệu lực trong 1 phút. Vui lòng không chia sẻ với bất kỳ ai." +
                "<br><h3>Mã xác minh của bạn là:<br>" + "<h1>" + Otp;
            await emailService.SendEmailAsync(receiver, subject, message);
            string encryptedEmail = EncryptEmail(Email);
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
    private string EncryptEmail(string emailAddress)
    {
        byte[] data = Encoding.UTF8.GetBytes(emailAddress);
        string encryptedEmail = Convert.ToBase64String(data);
        return encryptedEmail;
    }
}
