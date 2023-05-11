using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Service;

namespace TADA.Pages;

public class OrderListFillDoneModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;
    public const int ITEMS_PER_PAGE = 10;
    public int countPages { get; set; }
    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int currentPage { get; set; }
    public string Username;
    public List<OrderDto> Orders { get; set; }
    public BookDto Book { get; set; }
    public int statusId = 5;

    public OrderListFillDoneModel(IOrderService orderService, IAccountService accountService, IBookService bookService)
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
    public int GetOrderCountByStatus()
    {
        return orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusId).Count;
    }
    public void OnGet()
    {
        Username = HttpContext.Session.GetString("Name");
        var orders = orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusId);
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
