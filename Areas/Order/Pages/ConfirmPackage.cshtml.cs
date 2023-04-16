using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using TADA.Dto.Book;
using TADA.Dto.Customer;
using TADA.Dto.Order;
using TADA.Dto.Staff;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;

namespace TADA.Pages;

public class ConfirmPackageModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;
    private readonly ICustomerService customerService;
    private readonly IAddressService addressService;

    public OrderDto Order { get; set; } = null;
    public List<OrderDetailDto> OrderDetails { get; set; }
    public CustomerDto Customer { get; set; }
    public string Address { get; set; }
    public int statusId = 6;

    public ConfirmPackageModel(IOrderService orderService, IAccountService accountService, IBookService bookService, IAddressService addressService,ICustomerService customerService)
    {
        this.orderService = orderService;
        this.accountService = accountService;
        this.bookService = bookService;
        this.addressService = addressService;
        this.customerService = customerService;
    }
    public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
    {
        return orderService.GetBookByOrderDetail(orderDetail);
    }
    // Tinh trong service, nhet vao dto . Ai lai lam nhu ri
    public int SumPriceOfBooks()
    {
        int sum = 0;
        foreach (var orderDetail in OrderDetails)
        {
            sum += orderDetail.Price;
        }
        return sum;
    }
    public int SumPriceOfOrder()
    {
        int shipFee = 15000;
        return SumPriceOfBooks() + shipFee;
    }
    public string GetPriceString(int price)
    {
        string str = price.ToString();
        string tmp = "";
        while (str.Length > 3)
        {
            tmp = "." + str.Substring(str.Length - 3) + tmp;
            str = str.Substring(0, str.Length - 3);
        }
        tmp = str + tmp;
        return tmp;
    }
    public void DeleteOrder(OrderDto order)
    {
        orderService.DeleteOrder(order);
    }
    public string GetPartOfAddress(int part)
    {
        return addressService.GetAddressByIdAndPart(Customer.AddressId, part);
    }

    public void OnGet()
    {
        Order = orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusId).FirstOrDefault();
        OrderDetails = orderService.GetOrderDetailsByOrder(orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusId).FirstOrDefault());
        Customer = customerService.GetCustomerByAccountId((int)HttpContext.Session.GetInt32("Id"));
        Address = addressService.GetAddressById(Customer.AddressId);
    }
    public IActionResult OnPostUpdateStatusOrder()
    {
        orderService.UpdateStatusOrder(orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusId).FirstOrDefault().Id, 1);
        Console.WriteLine("OK");
        return RedirectToPage("/OrderListFillAll");
    }

}
