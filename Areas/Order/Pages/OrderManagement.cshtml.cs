using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using TADA.Dto.Order;
using TADA.Service;

namespace TADA.Pages;

public class OrderManagementModel : PageModel
{
    private readonly IOrderService orderService;
    public List<OrderManagementDto> Orders { get; set; }
    public int UserId { get; set; }
    public OrderManagementModel(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    public void OnGet()
    {
        var url = HttpContext.Request.GetDisplayUrl();
        var uri = new Uri(url);
        UserId = Convert.ToInt32(HttpUtility.ParseQueryString(uri.Query).Get("userId"));
        Orders = orderService.GetOrdersByCustomerId(UserId);
    }
}
