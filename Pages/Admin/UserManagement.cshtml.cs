using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto;
using TADA.Service;

namespace TADA.Pages;

public class UserModel : PageModel
{
    public readonly ICustomerService customerService;
    public List<CustomerDto> Customers { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Gender { get; set; }
    [BindProperty(SupportsGet = true)]
    public string Status { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "New";
    public string SortType { get; set; } = "Acs";

    public UserModel(ICustomerService customerService)
    {
        this.customerService = customerService;
    }

    public void OnGet()
    {
        Customers = customerService.GetCustomers(Gender, Status, SortBy, SortType);
    }
}
