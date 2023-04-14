using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;

namespace TADA.Pages;

public class OrderListFillAllModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;

    public string Username;
    public List<OrderDto> Orders { get; set; }
    public BookDto Book { get; set; }

    public OrderListFillAllModel(IOrderService orderService, IAccountService accountService, IBookService bookService)
    {
        this.orderService = orderService;
        this.accountService = accountService;
        this.bookService = bookService;
    }
    public List<OrderDetailDto> GetOrderDetailsDto(OrderDto order)
    {
        return orderService.GetOrderDetailsByOrder(order);
    }
    public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
    {
        return orderService.GetBookByOrderDetail(orderDetail);
    }
    public string GetStatusOfOrder(OrderDto order)
    {
        return orderService.GetStatusByOrder(order);
    }
    public int GetOrderCountByStatus(int statusId)
    {
        return orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusId).Count;
    }
    public void OnGet()
    {
        Username = HttpContext.Session.GetString("Name");
        Orders = orderService.GetAllOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"));

    }
}
