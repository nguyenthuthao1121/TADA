using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            var receiver = Email;
            var subject = "Mã xác minh mật khẩu";
            var message = "Đây là mã dùng một lần. Vui lòng không chia sẻ với bất kỳ ai." +
                "Mã xác minh của bạn là: 03102003";
            await emailService.SendEmailAsync(receiver, subject, message);
            return RedirectToPage("./ForgotPwd2");
        }
        else
        {
            Message = "Tài khoản email không tồn tại!";
            return Page();
        }
    }
}
