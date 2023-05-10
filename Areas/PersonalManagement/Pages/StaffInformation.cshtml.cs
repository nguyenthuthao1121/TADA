using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TADA.Dto;
using TADA.Dto.Address;
using TADA.Dto.Customer;
using TADA.Dto.Staff;
using TADA.Dto.Validation;
using TADA.Model.Entity;
using TADA.Service;

namespace TADA.Pages;

public class StaffInformationModel : PageModel
{
    private readonly IStaffService staffService;
    private readonly IAddressService addressService;
    private readonly IAccountService accountService;
    [BindProperty]
    public StaffDto Staff { get; set; }
    [BindProperty]
    public string StaffName { get; set; }
    public AddressDto Address { get; set; }
    public List<WardDto> Wards { get; set; }
    public List<DistrictDto> Districts { get; set; }
    public List<ProvinceDto> Provinces { get; set; }
    public string Gender { get; set; }

    [BindProperty]
    [Required]
    public int SelectedProvince { get; set; }
    [BindProperty]
    [Required]
    public int SelectedDistrict { get; set; }

    [BindProperty]
    [Required]
    public int SelectedWard { get; set; }

    public string Password { get; set; }

    [BindProperty]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    [Compare(nameof(Password), ErrorMessage = "Nhập mật khẩu hiện tại không chính xác!")]
    public string OldPassword { get; set; }

    [BindProperty]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài ít nhất là 6 ký tự!")]
    [NotEqual(ErrorMessage = "Mật khẩu mới không được trùng với mật khẩu cũ!")]

    public string NewPassword { get; set; }

    [BindProperty]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không đúng!")]
    public string ConfirmPassword { get; set; }


    public StaffInformationModel(IStaffService staffService, IAddressService addressService, IAccountService accountService)
    {
        this.staffService = staffService;
        this.addressService = addressService;
        this.accountService = accountService;
    }
    public void OnGet()
    {
        int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
        Staff = staffService.GetStaffDtoByAccountId(UserId);
        Address = addressService.GetStaffAddressDto(UserId);
        Provinces = addressService.GetAllProvinces();
        Districts = addressService.GetAllDistrictsByProvinceId(Address.ProvinceId);
        Wards = addressService.GetAllWardsByDistrictId(Address.DistrictId);
        Gender = Staff.Gender ? "Nam" : "Nữ";
        Password = Staff.Password;
    }
    public IActionResult OnPostChangeInformation()
    {
        Staff.AccountId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
        HttpContext.Session.SetString("Name", Staff.Name);
        Staff.WardId = SelectedWard;
        staffService.UpdateStaff(Staff);
        return RedirectToPage("./StaffInformation");
    }
    public IActionResult OnPostChangePassword()
    {
        accountService.ChangePassword(Convert.ToInt32(HttpContext.Session.GetInt32("Id")), NewPassword);
        return RedirectToPage("./StaffInformation");
    }
    public JsonResult OnGetDistricts(int SelectedProvince)
    {
        List<DistrictDto> districtDtos = new List<DistrictDto>();
        districtDtos = addressService.GetAllDistrictsByProvinceId(SelectedProvince);
        return new JsonResult(districtDtos);
    }
    public JsonResult OnGetWards(int SelectedDistrict)
    {
        List<WardDto> wardDtos = new List<WardDto>();
        wardDtos = addressService.GetAllWardsByDistrictId(SelectedDistrict);
        return new JsonResult(wardDtos);
    }

}
