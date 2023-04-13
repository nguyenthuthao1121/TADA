using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Account;
using TADA.Dto.Book;
using TADA.Dto.Category;
using TADA.Model;
using TADA.Service;

namespace TADA.Pages;

public class IndexModel : PageModel
{
    private readonly IBookService bookService;
    private readonly ICategoryService categoryService;
    private readonly IAccountService accountService;

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

    public IndexModel(IBookService bookService, ICategoryService categoryService, IAccountService accountService)
    {
        this.bookService = bookService;
        this.categoryService = categoryService;
        this.accountService = accountService;
    }

    public void OnGet()
    {
        Books = bookService.GetBooks(Category, PriceRange, SortBy);
        Categories = categoryService.GetAllCategories();
        Username = HttpContext.Session.GetString("Name");
    }


}
