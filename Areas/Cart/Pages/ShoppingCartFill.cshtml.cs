using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto;
using TADA.Dto.BookDto;
using TADA.Service;

namespace TADA.Pages;

public class ShoppingCartFillModel : PageModel
{
    private readonly ICartService cartService;
    private readonly IAccountService accountService;
    private readonly IBookService bookService;

    public string Username;
    public List<CartDetailDto> CartDetails { get; set; }

    public ShoppingCartFillModel(ICartService cartService, IAccountService accountService, IBookService bookService)
    {
        this.cartService = cartService;
        this.accountService = accountService;
        this.bookService = bookService;
    }
    public BookDto GetBookByCartDetail(CartDetailDto cartDetail)
    {
        return cartService.GetBookByCartDetail(cartDetail);
    }
    public void OnGet()
    {
        CartDetails = cartService.GetCartDetailsByAccountId((int)HttpContext.Session.GetInt32("Id"));
        Username = HttpContext.Session.GetString("Name");
    }
}
