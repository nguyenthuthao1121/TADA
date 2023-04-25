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
    public int CountBookOfCart { get; set; }

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
        CountBookOfCart = CartDetails.Count();
    }
    public void OnPostAddOrder(int BookId)
    {
        //List<OrderDetailDto> orderDetails = new List<OrderDetailDto>;
        //orderDetails.Add(new OrderDetailDto
        //{
        //    BookId = BookId,
        //    Price = 10,
        //    Quantity = 1,
        //});
        //orderService.AddOrder((int)HttpContext.Session.GetInt32("Id"), orderDetails);

    }
    public IActionResult OnPostDeleteBookOfCart(int? bookId)
    {
        if (bookId != null)
        {
            cartService.DeleteBookOfCart((int)bookId, (int)HttpContext.Session.GetInt32("Id"));

        }
        return RedirectToPage("/ShoppingCartFill");
    }
    public IActionResult OnPostMinusQuantity(int? bookId)
    {
        if (bookId != null)
        {
            cartService.DecreaseQuantityOfCartDetail((int)HttpContext.Session.GetInt32("Id"), (int)bookId, 1);
        }
        return RedirectToPage("/ShoppingCartFill");
    }
    public IActionResult OnPostPlusQuantity(int? bookId)
    {
        if (bookId != null)
        {
            cartService.IncreaseQuantityOfCartDetail((int)HttpContext.Session.GetInt32("Id"), (int)bookId, 1);
        }
        return RedirectToPage("/ShoppingCartFill");
    }
}
