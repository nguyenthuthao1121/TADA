using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Address;
using TADA.Dto.Staff;
using TADA.Service;

namespace TADA.Pages;

public class StaffModel : PageModel
{
    private readonly IStaffService staffService;
    private readonly IAddressService addressService;
    private readonly IRoleService roleService; 
    public List<StaffDto> Staffs { get; set; }
    public List<AddressDto> Addressses { get; set; }
    [BindProperty(SupportsGet = true)]
    public string Gender { get; set; }
    [BindProperty(SupportsGet = true)]
    public string Status { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "New";
    public string SortType { get; set; } = "Acs";

    public StaffModel(IStaffService staffService, IAddressService addressService, IRoleService roleService)
    {
        this.staffService = staffService;
        this.addressService = addressService;
        this.roleService = roleService;
    }

    public void OnGet()
    {
        Staffs = staffService.GetAllStaffs();
        foreach (var staff in Staffs)
        {
            staff.Address = addressService.GetAddressById(staff.AddressId);
            staff.RoleName = roleService.GetRoleNameById(staff.RoleId);
        }
    }
}
