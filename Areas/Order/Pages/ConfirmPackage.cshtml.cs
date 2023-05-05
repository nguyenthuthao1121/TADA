using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using TADA.Dto.Address;
using TADA.Dto.Book;
using TADA.Dto.Customer;
using TADA.Dto.Order;
using TADA.Dto.Staff;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;

namespace TADA.Pages;

public class ConfirmPackageModel : PageModel
{
    private readonly IOrderService orderService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;
    private readonly ICustomerService customerService;
    private readonly IAddressService addressService;

    [BindProperty]
    public OrderDto Order { get; set; } = null;
    public List<OrderDetailDto> OrderDetails { get; set; }
    public CustomerDto Customer { get; set; }
    public List<WardDto> Wards { get; set; }
    public List<DistrictDto> Districts { get; set; }
    public List<ProvinceDto> Provinces { get; set; }

    [BindProperty]
    public int SelectedProvince { get; set; }
    [BindProperty]
    public int SelectedDistrict { get; set; }

    [BindProperty]
    public int SelectedWard { get; set; }
    public AddressDto Address { get; set; }
    [BindProperty]
    public string UpdateTel { get; set; }


    [BindProperty]
    public string Street { get; set; }  
    public int StatusId = 6;

    public ConfirmPackageModel(IOrderService orderService, IAccountService accountService, IBookService bookService, IAddressService addressService,ICustomerService customerService)
    {
        this.orderService = orderService;
        this.accountService = accountService;
        this.bookService = bookService;
        this.addressService = addressService;
        this.customerService = customerService;
    }
    public BookDto GetBookByOrderDetail(OrderDetailDto orderDetail)
    {
        return orderService.GetBookByOrderDetail(orderDetail);
    }
    // Tinh trong service, nhet vao dto . Ai lai lam nhu ri
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
    public string GetPriceString(int price)
    {
        string str = price.ToString();
        string tmp = "";
        while (str.Length > 3)
        {
            tmp = "." + str.Substring(str.Length - 3) + tmp;
            str = str.Substring(0, str.Length - 3);
        }
        tmp = str + tmp;
        return tmp;
    }
    public void DeleteOrder(int orderId)
    {
        orderService.DeleteOrder(orderId);
    }
    public string GetPartOfAddress(int part)
    {
        return addressService.GetAddressByIdAndPart(Customer.AddressId, part);
    }

    public void OnGet()
    {

        Order = orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), StatusId).FirstOrDefault();
        OrderDetails = orderService.GetOrderDetailsByOrderId(orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), StatusId).FirstOrDefault().Id);
        int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
        Customer = customerService.GetCustomerByAccountId(UserId);
        Address = addressService.GetOrderAddressDto(Order.Id);
        Provinces = addressService.GetAllProvinces();
        Districts = addressService.GetAllDistrictsByProvinceId(Address.ProvinceId);
        Wards = addressService.GetAllWardsByDistrictId(Address.DistrictId);
        Street= Address.Street;
    }
    public IActionResult OnPostUpdateStatusOrder()
    {
        orderService.UpdateStatusOrder(orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), StatusId).FirstOrDefault().Id, 1);
        return RedirectToPage("/OrderListFillAll");
    }
    public IActionResult OnPostChangeInformation()
    {
        int addressId = 0;
        if (SelectedWard != 0)
            addressId=addressService.AddNewAddress(Street, SelectedWard);
        orderService.UpdateOrder(orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), StatusId).FirstOrDefault().Id, new OrderDto
        {
            Id =Order.Id,
            TelephoneNumber = Order.TelephoneNumber,
            AddressId = addressId,
        });
        return RedirectToPage("./ConfirmPackage");
    }
    //public IActionResult OnPostUpdateOrder(string username, string userTel, )
    //{
    //    //var orderUpdate = orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusId).FirstOrDefault();
    //    //if (orderUpdate != null)
    //    //{
    //    //    OrderDto order = new OrderDto
    //    //    {

    //    //    };
    //    //    orderService.UpdateOrder(orderUpdate.Id, order);
    //    //}
    //    return Page();
    //}
    public JsonResult OnGetDistricts(int SelectedProvince)
    {
        List<DistrictDto> districtDtos = new List<DistrictDto>();
        districtDtos = addressService.GetAllDistrictsByProvinceId(SelectedProvince);
        return new JsonResult(districtDtos);
    }
    public JsonResult OnGetWards(int SelectedDistrict)
    {
        List<WardDto> wardDtos = new List<WardDto>();
        wardDtos = addressService.GetAllWardsByDistrictId(SelectedDistrict);
        return new JsonResult(wardDtos);
    }
}
