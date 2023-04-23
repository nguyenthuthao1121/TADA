﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Service;

namespace TADA.Pages;

public class OrderListFillConfirmModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;

    public string Username;
    public List<OrderDto> Orders { get; set; }
    public BookDto Book { get; set; }
    public int statusId = 1;

    public OrderListFillConfirmModel(IOrderService orderService, IAccountService accountService, IBookService bookService)
    {
        this.orderService = orderService;
        this.accountService = accountService;
        this.bookService = bookService;
    }
    public List<OrderDetailDto> GetOrderDetailsDto(OrderDto order)
    {
        return orderService.GetOrderDetailsByOrderId(order.Id);
    }
    public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
    {
        return orderService.GetBookByOrderDetail(orderDetail);
    }
    public string GetStatusOfOrder(int orderId)
    {
        return orderService.GetStatusByOrder(orderId);
    }
    public int GetOrderCountByStatus(int statusId)
    {
        return orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusId).Count;
    }
    public void OnGet()
    {
        Username = HttpContext.Session.GetString("Name");
        Orders = orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"),statusId);

    }

}
