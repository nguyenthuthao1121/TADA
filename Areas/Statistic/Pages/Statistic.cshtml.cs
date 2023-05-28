using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TADA.Dto;
using TADA.Dto.Address;
using TADA.Dto.Customer;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Middleware;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên kinh doanh")]
public class StatisticModel : PageModel
{
    private readonly ICustomerService customerService;
    private readonly IStaffService staffService;
    private readonly IBookService bookService;
    private readonly IOrderService orderService;
    private readonly IStatisticService statisticService;
    public int NumOfCustomers { get; set; }
    public int NumOfStaffs { get; set; }
    public int NumOfBooks { get; set; }
    public int NumOfOrders { get; set; }
    public StatisticModel(ICustomerService customerService, IStaffService staffService, IBookService bookService, IOrderService orderService, IStatisticService statisticService)
    {
        this.customerService = customerService;
        this.staffService = staffService;
        this.bookService = bookService;
        this.orderService = orderService;
        this.statisticService = statisticService;
    }
    public void OnGet()
    {
        NumOfCustomers = customerService.GetNumOfCustomers();
        NumOfStaffs = staffService.GetNumOfStaffs();
        NumOfBooks = orderService.GetNumOfSoldBooks();
        NumOfOrders = orderService.GetNumOfOrders();
    }
    public ActionResult OnGetReportByYear(int year)
    {
        return new JsonResult(statisticService.GetReportByYear(year));   
    }
}
