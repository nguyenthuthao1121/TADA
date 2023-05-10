using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Policy;
using System.Web;
using TADA.Dto.Address;
using TADA.Dto.Customer;
using TADA.Middleware;
using TADA.Model.Entity;
using TADA.Service;
using static System.Reflection.Metadata.BlobBuilder;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên kinh doanh")]
public class CustomerModel : PageModel
{
    public readonly IAccountService accountService;
    public readonly ICustomerService customerService;
    public readonly IAddressService addressService;
    public List<CustomerDto> Customers { get; set; }
    public List<AddressDto> Addressses { get; set; }
    public const int ITEMS_PER_PAGE = 1;
    public int countPages { get; set; }
    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int currentPage { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SearchQuery { get; set; }
    [BindProperty(SupportsGet = true)]
    public string Gender { get; set; }
    [BindProperty(SupportsGet = true)]
    public string Status { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "New";
    [BindProperty(SupportsGet = true)]
    public string SortType { get; set; } = "Decs";

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
        var customers = customerService.GetCustomers(SearchQuery, Gender, Status, SortBy, SortType);
        int total = customers.Count();
        countPages = (int)Math.Ceiling((double)total / ITEMS_PER_PAGE);
        if (currentPage < 1)
        {
            currentPage = 1;
        }
        if (currentPage > countPages)
        {
            currentPage = countPages;
        }
        Customers = customers.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
        foreach (var customer in Customers)
        {
            customer.Address = addressService.GetAddressById(customer.AddressId);
        }
        
    }
}
