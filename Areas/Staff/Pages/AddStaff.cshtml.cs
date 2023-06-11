using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using TADA.Dto.Role;
using TADA.Dto.Staff;
using TADA.Service;
using TADA.Middleware;
using TADA.Utilities;

namespace TADA.Pages;

[Authorize("Quản trị viên")]
public class AddStaffModel : PageModel
{
    private readonly IAccountService accountService;
    private readonly IStaffService staffService;
    private readonly IRoleService roleService;
    private readonly IAuthenticationService authenticationService;
    [BindProperty(SupportsGet =true)]
    public AddStaffDto Staff { get; set; }
    public List<RoleDto> Roles { get; set; }
    public string Message { get; set; }
    public AddStaffModel(IAccountService accountService, IStaffService staffService, IRoleService roleService, IAuthenticationService authenticationService)
    {
        this.accountService = accountService;
        this.staffService = staffService;
        this.roleService = roleService;
        this.authenticationService = authenticationService;
    }
    public void OnGet()
    {
        Roles = roleService.GetAllRoles();
    }
    public IActionResult OnPost()
    {
        try
        {
            var account = authenticationService.GetAccountByEmail(Staff.Email);
            if (account != null)
            {
                Message = "Email đã được dùng để đăng ký tài khoản trước đó";
                OnGet();
                return Page();
            }
            else
            {
                AddStaffDto newStaff = new AddStaffDto
                {
                    Name = Staff.Name,
                    Gender = Staff.Gender,
                    Birthday = Staff.Birthday,
                    TelephoneNumber = Staff.TelephoneNumber,
                    RoleId = Staff.RoleId,
                    WardId = Staff.WardId,
                    Street = Staff.Street,
                    Email = Staff.Email,
                    Password = HashPassword.Hash(Staff.Password)
                };
                staffService.AddStaff(newStaff);
                return RedirectToPage("/StaffManagement", new { area = "Staff" });
            }
        }
        catch (Exception)
        {
            Message = "Thông tin nhân viên còn thiếu hoặc không hợp lệ";
            OnGet();
            return Page();
        }
    }
}
