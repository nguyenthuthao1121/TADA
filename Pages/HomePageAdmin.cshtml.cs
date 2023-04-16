using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Customer;
using TADA.Service;

namespace TADA.Pages;

public class HomePageAdminModel : PageModel
{
    private readonly ICustomerService customerService;
    public List<CustomerDto> Customers { get; set; }
    public HomePageAdminModel(ICustomerService customerService)
    {
        this.customerService = customerService;
    }

    public void OnGet()
    {
        Customers = customerService.GetAllCustomers().Take(10).ToList();
    }
}
