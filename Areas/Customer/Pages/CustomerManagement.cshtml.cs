using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using TADA.Dto.Address;
using TADA.Dto.Customer;
using TADA.Middleware;
using TADA.Service;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên kinh doanh")]
public class CustomerModel : PageModel
{
    public readonly IAccountService accountService;
    public readonly ICustomerService customerService;
    public readonly IAddressService addressService;
    public List<CustomerDto> Customers { get; set; }
    public List<AddressDto> Addressses { get; set; }
    [BindProperty(SupportsGet = true)]
    public string Gender { get; set; }
    [BindProperty(SupportsGet = true)]
    public string Status { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "New";
    public string SortType { get; set; } = "Acs";

    public CustomerModel(IAccountService accountService, ICustomerService customerService, IAddressService addressService)
    {
        this.accountService = accountService;
        this.customerService = customerService;
        this.addressService = addressService;
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
        Customers = customerService.GetCustomers(Gender, Status, SortBy, SortType);
        foreach(var customer in Customers)
        {
            customer.Address = addressService.GetAddressById(customer.AddressId);
        }
    }
}
