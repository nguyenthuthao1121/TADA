using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System.Net;
using TADA.Dto.Book;
using TADA.Dto.Cart;
using TADA.Dto.Order;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;

namespace TADA.Pages;

public class ShoppingCartFillModel : PageModel
{
    private readonly ICartService cartService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;
    private readonly IOrderService orderService;
    private readonly IStatusService statusService;

    [BindProperty(SupportsGet = true)]
    public List<CartDetailDto> CartDetails { get; set; }
    [BindProperty]
    public List<bool> IsSelected { get; set; }
    [BindProperty(SupportsGet = true)]
    public int CountBookOfCart { get; set; }
    [BindProperty]
    public int NumOfOrder { get; set; } = 0;
    [BindProperty]
    public int SumOfOrder { get; set; } = 0;

    public ShoppingCartFillModel(ICartService cartService, IAccountService accountService, IBookService bookService, IOrderService orderService, IStatusService statusService)
    {
        this.cartService = cartService;
        this.accountService = accountService;
        this.bookService = bookService;
        this.orderService = orderService;
        this.statusService = statusService;
    }
    public BookDto GetBookByCartDetail(CartDetailDto cartDetail)
    {
        return cartService.GetBookByCartDetail(cartDetail);
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

    public void OnGet()
    {
        CartDetails = cartService.GetCartDetailsByAccountId((int)HttpContext.Session.GetInt32("Id"));
        CountBookOfCart = CartDetails.Count();
        IsSelected = new List<bool>();
        for (int i=0;i<30;i++)
        {
            IsSelected.Add(false);
        }
        for(int i=0;i<CountBookOfCart;i++)
        {
            if (IsSelected[i])
            {
                var Book= bookService.GetBookById(CartDetails[i].BookId);
                NumOfOrder += CartDetails[i].Quantity;
                SumOfOrder += CartDetails[i].Quantity * Book.GetCurrentPrice();
            }
        }
    }

   
    public IActionResult OnPostOrderNow()
    {
        var details= cartService.GetCartDetailsByAccountId((int)HttpContext.Session.GetInt32("Id"));
        var orders = orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), statusService.IdForUserConfirm());
        if (orders != null)
        {
            foreach (var order in orders)
            {
                orderService.DeleteOrder(order.Id);
            }
        }
        List<OrderDetailDto> selectedOrderDetails= new List<OrderDetailDto>();
        for(int i=0;i< details.Count(); i++)
        {
            if (IsSelected[i]==true)
            {
                var bookOrder = bookService.GetBookById(details[i].BookId);
                selectedOrderDetails.Add(new OrderDetailDto
                {
                    OrderId = 1,
                    BookId = bookOrder.Id,
                    Quantity = Convert.ToInt32(details[i].Quantity),
                    Price = bookOrder.GetCurrentPrice() * Convert.ToInt32(details[i].Quantity),
                });
            }
        }
        
        if (selectedOrderDetails.Count > 0)
        {
            orderService.AddOrder((int)HttpContext.Session.GetInt32("Id"), selectedOrderDetails);
            return RedirectToPage("ConfirmPackage", new { area = "Order", message = "FromShoppingCart" });
        }
        else return RedirectToPage("/ShoppingCartFill");
    }
    public IActionResult OnPostDeleteBookOfCart()
    {
        var details= cartService.GetCartDetailsByAccountId((int)HttpContext.Session.GetInt32("Id"));
        for (int i = 0; i < details.Count(); i++)
        {
            cartService.DeleteBookOfCart(details[i].BookId, (int)HttpContext.Session.GetInt32("Id"));
        }
        return RedirectToPage("/ShoppingCartFill");
    }
    //public IActionResult OnPostMinusQuantity(int? bookId)
    //{
    //    if (bookId != null)
    //    {
    //        cartService.DecreaseQuantityOfCartDetail((int)HttpContext.Session.GetInt32("Id"), (int)bookId, 1);
    //    }
    //    return RedirectToPage("/ShoppingCartFill");
    //}
    //public IActionResult OnPostPlusQuantity(int? bookId)
    //{
    //    if (bookId != null)
    //    {
    //        cartService.IncreaseQuantityOfCartDetail((int)HttpContext.Session.GetInt32("Id"), (int)bookId, 1);
    //    }
    //    return RedirectToPage("/ShoppingCartFill");
    //}
    [HttpGet]
    public void OnGetUpdateQuantity()
    {
        if (int.TryParse(Request.Query["itemId"], out int itemId) && int.TryParse(Request.Query["quantity"], out int quantity))
        {
            CartDetails = cartService.GetCartDetailsByAccountId((int)HttpContext.Session.GetInt32("Id"));
            CountBookOfCart = CartDetails.Count();
            if (CartDetails[itemId].BookId > 0)
            {
                cartService.UpdateQuantityOfCartDetail((int)HttpContext.Session.GetInt32("Id"), CartDetails[itemId].BookId, quantity);
            }
        }
    }
    [HttpGet]
    public void OnGetDeleteCartDetail()
    {
        if (int.TryParse(Request.Query["itemId"], out int itemId))
        {
            CartDetails= cartService.GetCartDetailsByAccountId((int)HttpContext.Session.GetInt32("Id"));
            CountBookOfCart = CartDetails.Count()-1;
            if (CartDetails[itemId].BookId > 0)
            {
                cartService.DeleteBookOfCart(CartDetails[itemId].BookId, (int)HttpContext.Session.GetInt32("Id"));
            }

        }
    }
}
