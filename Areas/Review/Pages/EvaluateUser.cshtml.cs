using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;
using TADA.Utilities;

namespace TADA.Pages;

public class EvaluateUserModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IReviewService reviewService;
    private readonly IOrderService orderService;

    public EvaluateUserModel(IBookService bookService, IReviewService reviewService, IOrderService orderService)
    {
        this.bookService = bookService;
        this.reviewService = reviewService;
        this.orderService = orderService;
    }

    public OrderDto Order { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
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
            Order=orderService.GetOrderById(orderId);
            OrderDetails=orderService.GetOrderDetailsByOrderId(orderId);
            
        }
    }
}
