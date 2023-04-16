using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using TADA.Dto.Book;
using TADA.Dto.Cart;
using TADA.Dto.Order;
using TADA.Model.Entity;
using TADA.Service;

namespace TADA.Pages;

public class ShoppingCartFillModel : PageModel
{
    private readonly ICartService cartService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;
    private readonly IOrderService orderService;

    public List<CartDetailDto> CartDetails { get; set; }

    public ShoppingCartFillModel(ICartService cartService, IAccountService accountService, IBookService bookService, IOrderService orderService)
    {
        this.cartService = cartService;
        this.accountService = accountService;
        this.bookService = bookService;
        this.orderService = orderService;
    }
    public BookDto GetBookByCartDetail(CartDetailDto cartDetail)
    {
        return cartService.GetBookByCartDetail(cartDetail);
    }

    public void OnGet()
    {
        CartDetails = cartService.GetCartDetailsByAccountId((int)HttpContext.Session.GetInt32("Id"));

    }
    public void OnPostAddOrder(int BookId)
    {
        var order = orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), 6).FirstOrDefault();
        foreach (var cartDetail in CartDetails)
        {
            if (cartDetail.BookId == BookId)
            {
                orderService.AddOrderDetail(new OrderDetailDto
                {
                    BookId = BookId,
                    OrderId = order.Id,
                    Price = 10,
                    Quantity = cartDetail.Quantity,
                });
                break;
            }
        }

    }
}
