using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TADA.Dto;
using TADA.Model;
using TADA.Service;
using TADA.Utilities;

namespace TADA.Pages;

public class LoginModel : PageModel
{
    private readonly IAuthenticationService authenticationService;
    private readonly ICustomerService customerService;
    private readonly IStaffService staffService;

    [BindProperty]
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Email này không phải là email hợp lệ!")]
    public string Email { get; set; }
    [BindProperty]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài tối thiểu là 6 ký tự!")]
    public string Password { get; set; }

    public string Message;

    public LoginModel(IAuthenticationService authenticationService, ICustomerService customerService, IStaffService staffService)
    {
        this.authenticationService = authenticationService;
        this.customerService = customerService;
        this.staffService = staffService;
    }

    public void OnGet()
    {
        
    }
    public void OnGetLogout()
    {
        HttpContext.Session.Clear();
    }
    public IActionResult OnPost()
    {
        var account = authenticationService.GetAccount(Email, HashPassword.Hash(Password));
        if (account != null)
        {
            if (account.Status == true)
            {
                HttpContext.Session.SetInt32("Id", account.Id);
                HttpContext.Session.SetString("Type", account.Type ? "Customer" : "Staff");
                if (account.Type)
                {
                    HttpContext.Session.SetString("Name", customerService.GetNameByAccountId(account.Id));
                    return RedirectToPage("/Index");
                }
                else
                {
                    var staff = staffService.GetStaffByAccountId(account.Id);
                    HttpContext.Session.SetString("Name", staff.Name);
                    HttpContext.Session.SetString("Role", staff.RoleName);
                    return RedirectToPage("/HomePageAdmin");
                }
            }
            else
            {
                Message = "Tài khoản này đã bị chặn vì vi phạm Tiêu chuẩn cộng đồng của chúng tôi";
                return Page();
            }
        }
        else
        {
            Message = "Tài khoản không tồn tại hoặc mật khẩu không đúng";
            return Page();
        }
    }
}
