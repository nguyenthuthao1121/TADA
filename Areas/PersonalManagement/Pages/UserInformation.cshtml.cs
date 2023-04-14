using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using System.Security.Claims;
using TADA.Dto;
using TADA.Dto.CustomerDto;
using TADA.Service;

namespace TADA.Pages;

public class UserInformationModel : PageModel
{
    private readonly ICustomerService customerService;
    private readonly IAddressService addressService;

    public CustomerDto Customer { get; set; }
    public string Address { get; set; }

    public string Username;
    public UserInformationModel(ICustomerService customerService, IAddressService addressService)
    {
        this.customerService = customerService;
        this.addressService = addressService;
    }
    public void OnGet()
    {
        Username = HttpContext.Session.GetString("Name");
        int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
        Customer = customerService.GetCustomerByAccountId(UserId);
        Address = addressService.GetCustomerAddressByAccountId(UserId);
    }
}
