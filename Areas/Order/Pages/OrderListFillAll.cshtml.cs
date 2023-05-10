﻿using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;
using static System.Reflection.Metadata.BlobBuilder;

namespace TADA.Pages;

public class OrderListFillAllModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;
    public const int ITEMS_PER_PAGE = 1;
    public int countPages { get; set; }
    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int currentPage { get; set; }
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
        var orders = orderService.GetAllOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"));
        int total = orders.Count();
        countPages = (int)Math.Ceiling((double)total / ITEMS_PER_PAGE);
        if (currentPage < 1)
        {
            currentPage = 1;
        }
        if (currentPage > countPages)
        {
            currentPage = countPages;
        }
        Orders = orders.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
    }
}
