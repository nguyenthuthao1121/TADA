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
    public const int ITEMS_PER_PAGE = 10;
    public int countPages { get; set; }
    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int currentPage { get; set; }
    public List<OrderManagementDto> Orders { get; set; }
    public int UserId { get; set; }
    public List<ProvinceDto> Provinces { get; set; }
    public List<StatusDto> Statuses { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "Desc";
    [BindProperty(SupportsGet = true)]
    public string PriceRange { get; set; } = "All";
    [BindProperty(SupportsGet = true)]
    public string Province { get; set; } = "All";
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
        var orders = orderService.GetOrdersByCustomerId(UserId, Province, PriceRange, Convert.ToInt32(StatusId), SortBy);
        Provinces = addressService.GetAllProvinces();
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
        Statuses = statusService.GetStatuses();
    }
    public IActionResult OnPostViewOrderByUser(int? id)
    {
       
        HttpContext.Session.SetInt32("UserId", UserId);
        return RedirectToPage("OrderDetailAdmin", new { id = id });
    }
}
