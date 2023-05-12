using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using TADA.Dto.Role;
using TADA.Dto.Staff;
using TADA.Service;
using TADA.Middleware;

namespace TADA.Pages;

[Authorize("Quản trị viên")]
public class AddStaffModel : PageModel
{
    private readonly IAccountService accountService;
    private readonly IStaffService staffService;
    private readonly IRoleService roleService;
    [BindProperty(SupportsGet =true)]
    public AddStaffDto Staff { get; set; }
    public List<RoleDto> Roles { get; set; }
    public string Message { get; set; }
    public AddStaffModel(IAccountService accountService, IStaffService staffService, IRoleService roleService)
    {
        this.accountService = accountService;
        this.staffService = staffService;
        this.roleService = roleService;
    }
    public void OnGet()
    {
        Roles = roleService.GetAllRoles();
    }
    public IActionResult OnPost()
    {
        try
        {
            if (Staff.Password != Staff.ConfirmPassword)
            {
                Message = "Mật khẩu nhập lại không trùng khớp";
            }
            else
            {
                if (accountService.CheckExistEmail(Staff.Email))
                {
                    Message = "Email đã được dùng để đăng ký tài khoản trước đó";
                }
                else
                {
                    staffService.AddStaff(Staff);
                    return RedirectToPage("/StaffManagement", new { area = "Staff" });
                }
            }
            return Page();
        }
        catch (Exception)
        {
            Message = "Thông tin nhân viên còn thiếu hoặc không hợp lệ";
            return Page();
        }
    }
}
