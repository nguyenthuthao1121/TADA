using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto;
using TADA.Service;

namespace TADA.Pages;

public class OrderListEmptyDoneModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly IAccountService accountService;

    public string Username;
    public BookDto Book { get; set; }

    public OrderListEmptyDoneModel(IOrderService orderService, IAccountService accountService)
    {
        this.orderService = orderService;
        this.accountService = accountService;
    }
    public List<OrderDetailDto> GetOrderDetailsDto(OrderDto order)
    {
        return orderService.GetOrderDetailsByOrder(order);
    }
    public int GetOrderCountByStatus(int statusId)
    {
        return orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusId).Count;
    }
    public void OnGet()
    {
        Username = HttpContext.Session.GetString("Name");
    }
}
