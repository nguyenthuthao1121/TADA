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

    [BindProperty(SupportsGet = true)]
    public CartDto Cart {get;set;}
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
        Cart = cartService.GetCartByAccountId((int)HttpContext.Session.GetInt32("Id"));
        CartDetails = new List<CartDetailDto>();
        CountBookOfCart = Cart.CartDetails.Count();
    }
    [HttpPost]
    public void OnPostAddBookToOrder(int bookId,bool isChecked)
    {
        var cartDetail = cartService.GetCartDetail((int)HttpContext.Session.GetInt32("Id"), bookId);
        if (isChecked)
        {
            CartDetails.Add(cartDetail);
        }
    }
    public IActionResult OnPostOrderNow()
    {
        var orders = orderService.GetOrdersByAccountId((int)HttpContext.Session.GetInt32("Id"), 6);
        if (orders != null)
        {
            foreach (var order in orders)
            {
                orderService.DeleteOrder(order.Id);
            }
        }
        List<OrderDetailDto> selectedOrderDetails= new List<OrderDetailDto>();
        // selectedCartDetails= Cart.CartDetails.Where(x => x.Selected).ToList();
        foreach (var cartDetail in CartDetails)
        {
            var bookOrder = bookService.GetBookById(cartDetail.BookId);
            selectedOrderDetails.Add(new OrderDetailDto
            {
                OrderId = 1,
                BookId = bookOrder.Id,
                Quantity = Convert.ToInt32(cartDetail.Quantity),
                Price = bookOrder.GetCurrentPrice() * Convert.ToInt32(cartDetail.Quantity),
            });
            //var bookOrder = bookService.GetBookById(cartDetail.BookId);
            //selectedOrderDetails.Add(new OrderDetailDto
            //{
            //    OrderId = 1,
            //    BookId = bookOrder.Id,
            //    Quantity = Convert.ToInt32(cartDetail.Quantity),
            //    Price = bookOrder.GetCurrentPrice() * Convert.ToInt32(cartDetail.Quantity),
            //});
        }
        //foreach (var item in selectedItems)
        //{
        //    var bookOrder = bookService.GetBookById(Convert.ToInt32(item));
        //    var cartDetail = cartService.GetCartDetail((int)HttpContext.Session.GetInt32("Id"), Convert.ToInt32(item));
        //    selectedOrderDetails.Add(new OrderDetailDto
        //    {
        //        OrderId = 1,
        //        BookId = bookOrder.Id,
        //        Quantity = Convert.ToInt32(cartDetail.Quantity),
        //        Price = bookOrder.GetCurrentPrice() * Convert.ToInt32(cartDetail.Quantity),
        //    });
        //}
        if (selectedOrderDetails.Count > 0)
        {
            orderService.AddOrder((int)HttpContext.Session.GetInt32("Id"), selectedOrderDetails);
            return RedirectToPage("ConfirmPackage", new { area = "Order" });
        }
        return RedirectToPage("/ShoppingCartFill");
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
