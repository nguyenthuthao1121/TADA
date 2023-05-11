using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using TADA.Dto.Book;
using TADA.Dto.Customer;
using TADA.Dto.Order;
using TADA.Service;
using TADA.Service.Implement;

namespace TADA.Pages;

public class OrderDetailConfirmModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;
    private readonly ICustomerService customerService;
    private readonly IAddressService addressService;
    private readonly IReviewService reviewService;

    public string Username;
    public OrderDto Order { get; set; } = null;
    public List<OrderDetailDto> OrderDetails { get; set; }
    public CustomerDto Customer { get; set; }
    public string Address { get; set; }
    public int statusId { get; set; }
    public string GetStatusOfOrder(OrderDto order)
    {
        string status= orderService.GetStatusByOrder(order.Id);
        while (status[status.Length-1]==' ') status=status.Substring(0,status.Length-1);
        return status;
    }
    public OrderDetailConfirmModel(IOrderService orderService, IAccountService accountService, IBookService bookService, IAddressService addressService, ICustomerService customerService, IReviewService reviewService)
    {
        this.orderService = orderService;
        this.accountService = accountService;
        this.bookService = bookService;
        this.addressService = addressService;
        this.customerService = customerService;
        this.reviewService = reviewService;
    }
    public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
    {
        return orderService.GetBookByOrderDetail(orderDetail);
    }
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
        return SumPriceOfBooks() + Order.ShipFee;
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
    public void DeleteOrder(int orderId)
    {
        orderService.DeleteOrder(orderId);
    }
    public string GetPartOfAddress(int part)
    {
        return addressService.GetAddressByIdAndPart(Customer.AddressId, part);
    }
    public bool BookIsReviewed(int orderId, int bookId)
    {
        if(reviewService.GetReview(orderId, bookId)!=null) return true;
        return false;
    }
    public void OnGet()
    {
        if (int.TryParse(Request.Query["id"], out int orderId))
        {
            Username = HttpContext.Session.GetString("Name");
            Order = orderService.GetOrderById(orderId);
            if (Order == null) { statusId = 1; }
            else statusId = Order.StatusId;
            OrderDetails = orderService.GetOrderDetailsByOrderId(orderService.GetOrderById(orderId).Id);
            Customer = customerService.GetCustomerByAccountId((int)HttpContext.Session.GetInt32("Id"));
            Address = addressService.GetAddressById((int)Order.AddressId);
        }
            
    }
    public IActionResult OnPostCancelOrder(int? id)
    {
        orderService.UpdateStatusOrder((int)id, 6);
        return RedirectToPage("OrderListFillCancel", new {area="Order"});
    }
}
