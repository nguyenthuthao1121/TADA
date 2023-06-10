using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TADA.Service;
using TADA.Utilities;

namespace TADA.Pages;

public class ForgotPwd2Model : PageModel
{
    private readonly IAccountService accountService;
    private readonly IEmailService emailService;

    [BindProperty]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài ít nhất là 6 ký tự!")]
    public string NewPassword { get; set; }

    [BindProperty]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không đúng!")]
    public string ConfirmPassword { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    public string Otp { get; set; }
    [BindProperty(SupportsGet = true, Name = "email")]
    public string EncryptedEmail { get; set; }

    public ForgotPwd2Model(IAccountService accountService, IEmailService emailService)
    {
        this.accountService = accountService;
        this.emailService = emailService;
    }

    public void OnGet()
    {
        TempData["Email"] = emailService.DecryptEmail(EncryptedEmail);
    }

    public IActionResult OnPost()
    {
        TempData["Email"] = emailService.DecryptEmail(EncryptedEmail);
        string rightOtp = HttpContext.Session.GetString("ResetPasswordOtp");
        DateTime otpExpiry = DateTime.Parse(HttpContext.Session.GetString("OtpExpiry"));
        if (!Otp.Equals(rightOtp))
        {
            TempData["WrongOtp"] = "Mã xác minh không chính xác";
            return Page();
        }
        if (DateTime.Now < otpExpiry)
        {
            int accountId = accountService.GetAccountIdByEmail(TempData["Email"].ToString());
            accountService.ChangePassword(accountId, HashPassword.Hash(NewPassword));
            TempData["ResetSuccessMessage"] = "Đổi mật khẩu thành công. Vui lòng đăng nhập để tiếp tục!";
            return Page();
        }
        else
        {
            TempData["ResetFailedMessage"] = "Mã xác minh đã hết hiệu lực. Vui lòng thử lại sau!";
            return Page();
        }
    }
}
