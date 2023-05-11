using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Web;
using TADA.Dto.Book;
using TADA.Dto.Customer;
using TADA.Dto.Order;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Middleware;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class OrderDetailAdminModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly ICustomerService customerService;
    private readonly IAddressService addressService;
    public OrderDetailAdminModel(ICustomerService customerService, IOrderService orderService, IAddressService addressService)
    {
        this.orderService = orderService;
        this.customerService = customerService;
        this.addressService = addressService;
    }
    [BindProperty]
    public OrderDto Order { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
    public CustomerDto Customer { get; set; }
    public int CustomerId { get; set; }
    public string Status { get; set; }

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

    public void OnGet()
    {
        if (int.TryParse(Request.Query["id"], out int orderId))
        {
            Order = orderService.GetOrderById(orderId);
            OrderDetails = orderService.GetOrderDetailsByOrderId(orderId);
            CustomerId = Order.CustomerId;
            Customer = customerService.GetCustomerById(CustomerId);
            Order.Address = addressService.GetAddressById((int)Order.AddressId);
            Status = orderService.GetStatusByOrder(orderId);
        }
        
    }
    public void OnGetAllOrder()
    {
        var url = HttpContext.Request.GetDisplayUrl();
        var uri = new Uri(url);
        if (int.TryParse(HttpUtility.ParseQueryString(uri.Query).Get("orderId"), out int orderId))
        {
            Order = orderService.GetOrderById(orderId);
            OrderDetails = orderService.GetOrderDetailsByOrderId(orderId);
            CustomerId=0;
            Customer = customerService.GetCustomerById(Order.CustomerId);
            Order.Address = addressService.GetAddressById((int)Order.AddressId);
            Status = orderService.GetStatusByOrder(orderId);
        }

    }
    public IActionResult OnPostCancelOrder(int? orderId, int? userId)
    {
        orderService.UpdateStatusOrder((int)orderId, 6);
        return RedirectToPage("OrderDetailAdmin", new { id = orderId });
    }
    public IActionResult OnPostConfirmOrder(int? orderId)
    {
        orderService.UpdateStatusOrder((int)orderId, 2);
        return RedirectToPage("OrderDetailAdmin", new { id = orderId });
    }
    public IActionResult OnPostDeliverNow(int? orderId)
    {
        orderService.UpdateStatusOrder((int)orderId, 3);
        return RedirectToPage("OrderDetailAdmin", new { id = orderId });
    }
    public IActionResult OnPostConfirmDelivered(int? orderId)
    {
        orderService.UpdateStatusOrder((int)orderId, 4);
        return RedirectToPage("OrderDetailAdmin", new { id = orderId });
    }
    public IActionResult OnPostDeliveryFail(int? orderId)
    {
        orderService.UpdateStatusOrder((int)orderId, 6);
        return RedirectToPage("OrderDetailAdmin", new { id = orderId });
    }
}
