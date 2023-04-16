using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Account;
using TADA.Dto.Book;
using TADA.Dto.Category;
using TADA.Model;
using TADA.Service;
using TADA.Service.Implement;

namespace TADA.Pages;

public class IndexModel : PageModel
{
    private readonly IBookService bookService;
    private readonly ICategoryService categoryService;
    private readonly IAccountService accountService;

    public const int ITEMS_PER_PAGE = 12;
    public int countPages { get; set; }
    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int currentPage { get; set; }
    public List<BookDto> Books { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public AccountDto Account { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Category { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "New";
    [BindProperty(SupportsGet = true)]
    public string PriceRange { get; set; } = string.Empty;

    public IndexModel(IBookService bookService, ICategoryService categoryService, IAccountService accountService)
    {
        this.bookService = bookService;
        this.categoryService = categoryService;
        this.accountService = accountService;
    }

    public void OnGet()
    {
        var books = bookService.GetBooks(Category, PriceRange, SortBy);

        Categories = categoryService.GetAllCategories();
        int totalBook = books.Count();
        countPages = (int)Math.Ceiling((double)totalBook / ITEMS_PER_PAGE);
        if (currentPage < 1)
        {
            currentPage = 1;
        }
        if (currentPage > countPages)
        {
            currentPage = countPages;
        }
        Books = books.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
    }


}
