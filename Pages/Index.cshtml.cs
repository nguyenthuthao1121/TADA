using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto;
using TADA.Model;
using TADA.Service;
using TADA.Service.Implement;

namespace TADA.Pages;

public class IndexModel : PageModel
{
    private readonly IBookService bookService;
    private readonly ICategoryService categoryService;
    private readonly IAccountService accountService;
    private readonly ICartService cartService;
    private readonly IOrderService orderService;

    public List<BookDto> Books { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public AccountDto Account { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Category { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "New";
    [BindProperty(SupportsGet = true)]
    public string PriceRange { get; set; } = string.Empty;
    public string Username;
    public int CountBookOfCart;
    public int CountOrder;

    public IndexModel(IBookService bookService, ICategoryService categoryService, IAccountService accountService, ICartService cartService, IOrderService orderService)
    {
        this.bookService = bookService;
        this.categoryService = categoryService;
        this.accountService = accountService;
        this.cartService = cartService;
        this.orderService = orderService;
    }

    public void OnGet()
    {
        Books = bookService.GetBooks(Category, PriceRange, SortBy);
        Categories = categoryService.GetAllCategories();
        Username = HttpContext.Session.GetString("Name");
        if (HttpContext.Session.GetInt32("Id") == null)
        {
            CountBookOfCart = 0;
            CountOrder= 0;
        }
        else
        {
            CountBookOfCart = cartService.GetCartDetailsByAccountId((int)HttpContext.Session.GetInt32("Id")).Count;
            CountOrder = orderService.GetAllOrdersByAccountId((int)HttpContext.Session.GetInt32("Id")).Count;
        }
    }


}
