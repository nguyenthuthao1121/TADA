using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Address;
using TADA.Dto.Customer;
using TADA.Middleware;
using TADA.Service;

namespace TADA.Pages;

[Authorize("Admin", "SalesStaff")]
public class UserModel : PageModel
{
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

    public UserModel(ICustomerService customerService, IAddressService addressService)
    {
        this.customerService = customerService;
        this.addressService = addressService;
    }

    public void OnGet()
    {
        Customers = customerService.GetCustomers(Gender, Status, SortBy, SortType);
        foreach(var customer in Customers)
        {
            customer.Address = addressService.GetAddressById(customer.AddressId);
        }
    }
}
