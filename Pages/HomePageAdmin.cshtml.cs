using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Customer;
using TADA.Dto.Order;
using TADA.Service;

namespace TADA.Pages;

public class HomePageAdminModel : PageModel
{
    private readonly ICustomerService customerService;
    private readonly IOrderService orderService;
    private readonly IBookService bookService;
    public List<CustomerDto> Customers { get; set; }
    public List<RecentlyOrderDto> Orders { get; set; }
    public List<SoldBookDto> Books { get; set; }
    public int Revuene { get; set; }
    public HomePageAdminModel(ICustomerService customerService, IOrderService orderService, IBookService bookService)
    {
        this.customerService = customerService;
        this.orderService = orderService;
        this.bookService = bookService;
    }

    public void OnGet()
    {
        Customers = customerService.GetAllCustomers().Take(10).ToList();
        Orders = orderService.GetRecentlyOrders(4);
        Books = bookService.GetSoldBooks().Take(5).ToList();
        Revuene = orderService.RevueneOfMonth(4, DateTime.Now.Year);
    }
}
