using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Web;
using TADA.Dto.Address;
using TADA.Dto.Order;
using TADA.Dto.Status;
using TADA.Middleware;
using TADA.Model.Entity;
using TADA.Service;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class OrderManagementModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly IAddressService addressService;
    private readonly IStatusService statusService;
    public List<OrderManagementDto> Orders { get; set; }
    public int UserId { get; set; }
    public List<ProvinceDto> Provinces { get; set; }
    public List<StatusDto> Statuses { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "Desc";
    [BindProperty(SupportsGet = true)]
    public string PriceRange { get; set; } = string.Empty;
    [BindProperty(SupportsGet = true)]
    public string Province { get; set; } = string.Empty;
    [BindProperty(SupportsGet = true)]
    public string StatusId { get; set; } = "0";

    public OrderManagementModel(IOrderService orderService, IAddressService addressService, IStatusService statusService)
    {
        this.orderService = orderService;
        this.addressService = addressService;
        this.statusService = statusService;
    }

    public void OnGet()
    {
        var url = HttpContext.Request.GetDisplayUrl();
        var uri = new Uri(url);
        UserId = Convert.ToInt32(HttpUtility.ParseQueryString(uri.Query).Get("userId"));
        Orders = orderService.GetOrdersByCustomerId(UserId, Province, PriceRange, Convert.ToInt32(StatusId), SortBy);
        Provinces = addressService.GetAllProvinces();
        Statuses = statusService.GetStatuses();
    }
    public IActionResult OnPostViewOrderByUser(int? id)
    {
       
        HttpContext.Session.SetInt32("UserId", UserId);
        return RedirectToPage("OrderDetailAdmin", new { id = id });
    }
}
