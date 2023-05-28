using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TADA.Dto;
using TADA.Dto.Account;
using TADA.Dto.Address;
using TADA.Dto.Customer;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Utilities;

namespace TADA.Pages;

public class UserInformationModel : PageModel
{
    private readonly ICustomerService customerService;
    private readonly IAddressService addressService;
    private readonly IAccountService accountService;

    public string Message { get; set; }
    [BindProperty]
    public CustomerDto Customer { get; set; }
    [BindProperty]
    public string CustomerName { get; set; }
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
    public UserInformationModel(ICustomerService customerService, IAddressService addressService, IAccountService accountService)
    {
        this.customerService = customerService;
        this.addressService = addressService;
        this.accountService = accountService;
    }
    public void OnGet()
    {
        int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
        Customer = customerService.GetCustomerByAccountId(UserId);
        Address = addressService.GetCustomerAddressDto(UserId);
        Provinces = addressService.GetAllProvinces();
        Districts = addressService.GetAllDistrictsByProvinceId(Address.ProvinceId);
        Wards = addressService.GetAllWardsByDistrictId(Address.DistrictId);
        Gender = Customer.Gender ? "Nam" : "Nữ";
        if (Customer.Name == "Khách hàng")
        {
            Message = "Vui lòng cập nhật thông tin cá nhân của bạn !";
        }
    }
    public IActionResult OnPostChangeInformation()
    {
        Customer.AccountId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
        HttpContext.Session.SetString("Name", Customer.Name);
        Customer.WardId = SelectedWard;
        customerService.UpdateCustomer(Customer);
        return RedirectToPage("./UserInformation");
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
        return RedirectToPage("./UserInformation");
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
