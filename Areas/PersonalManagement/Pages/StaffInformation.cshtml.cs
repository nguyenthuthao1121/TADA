using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TADA.Dto;
using TADA.Dto.Account;
using TADA.Dto.Address;
using TADA.Dto.Customer;
using TADA.Dto.Staff;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Utilities;

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
    [BindProperty]
    public PasswordDto ChangedPwd { get; set; }

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
        string oldPassword = accountService.GetPasswordByAccountId(Convert.ToInt32(HttpContext.Session.GetInt32("Id")));
        string inputPassword = HashPassword.Hash(ChangedPwd.OldPassword);
        string newPassword = HashPassword.Hash(ChangedPwd.NewPassword);
        if (inputPassword != oldPassword || newPassword == oldPassword)
        {
            TempData["Display"] = true;
            if (inputPassword != oldPassword)
            {
                TempData["ErrorMsg"] = "Mật khẩu hiện tại không chính xác";

            }
            if (newPassword == oldPassword)
            {
                TempData["ErrorMsg2"] = "Mật khẩu mới không được trùng với mật khẩu cũ";
            }
            OnGet();
            return Page();
        }
        accountService.ChangePassword(Convert.ToInt32(HttpContext.Session.GetInt32("Id")), ChangedPwd.NewPassword);
        TempData["Toast"] = true;
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
