using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TADA.Dto;
using TADA.Dto.Address;
using TADA.Dto.Customer;
using TADA.Dto.Validation;
using TADA.Model.Entity;
using TADA.Service;

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
        Password = Customer.Password;
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
        accountService.ChangePassword(Convert.ToInt32(HttpContext.Session.GetInt32("Id")), NewPassword);
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
