using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using TADA.Dto.Address;
using TADA.Dto.Staff;
using TADA.Middleware;
using TADA.Service;
using TADA.Service.Implement;

namespace TADA.Pages;

[Authorize("Quản trị viên")]
public class StaffModel : PageModel
{
    private readonly IAccountService accountService;
    private readonly IStaffService staffService;
    private readonly IAddressService addressService;
    private readonly IRoleService roleService;
    public const int ITEMS_PER_PAGE = 20;
    public int countPages { get; set; }
    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int currentPage { get; set; }
    public List<StaffDto> Staffs { get; set; }
    public List<AddressDto> Addressses { get; set; }
    [BindProperty(SupportsGet = true)]
    public string Gender { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; }
    [BindProperty(SupportsGet = true)]

    public string SortType { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SearchQuery { get; set; }
    [BindProperty(SupportsGet = true)]
    public string Filter { get; set; }

    public StaffModel(IAccountService accountService, IStaffService staffService, IAddressService addressService, IRoleService roleService)
    {
        this.accountService = accountService;
        this.staffService = staffService;
        this.addressService = addressService;
        this.roleService = roleService;
    }

    public void OnGet()
    {
        var url = HttpContext.Request.GetDisplayUrl();
        var uri = new Uri(url);
        var AccountId = Convert.ToInt32(HttpUtility.ParseQueryString(uri.Query).Get("userId"));
        if (AccountId > 0)
        {
            accountService.ChangeStatusOfAccount(AccountId);
        }
        var staff = staffService.GetStaff(SearchQuery, Filter, SortBy, SortType);
        int total = staff.Count();
        countPages = (int)Math.Ceiling((double)total / ITEMS_PER_PAGE);
        if (currentPage < 1)
        {
            currentPage = 1;
        }
        if (currentPage > countPages)
        {
            currentPage = countPages;
        }
        Staffs = staff.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
        foreach (var sff in Staffs)
        {
            sff.Address = addressService.GetAddressById(sff.AddressId);
            sff.RoleName = roleService.GetRoleNameById(sff.RoleId);
        }
    }
}
