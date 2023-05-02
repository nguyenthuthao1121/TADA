using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Policy;
using TADA.Dto.Account;
using TADA.Dto.Book;
using TADA.Dto.Category;
using TADA.Model;
using TADA.Model.Entity;
using TADA.Service;
using TADA.Service.Implement;
using TADA.Utilities;

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
    public string SearchUrl { get; set; }
    public string SearchQuery { get; set; }

    public int Category { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "New";
    [BindProperty(SupportsGet = true)]
    public string PriceRange { get; set; } = string.Empty;
    [BindProperty(SupportsGet = true)]
    public string Provider { get; set; } = string.Empty;
    [BindProperty(SupportsGet = true)]
    public string Genre { get; set; } = string.Empty;

    public IndexModel(IBookService bookService, ICategoryService categoryService, IAccountService accountService)
    {
        this.bookService = bookService;
        this.categoryService = categoryService;
        this.accountService = accountService;
    }

    public void OnGet()
    {
        Category = Convert.ToInt32(Request.Query["category"]);
        SearchQuery = Request.Query["q"];
        var books = bookService.GetBooks(Category, SearchQuery, PriceRange, Genre, SortBy);
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
    //public IActionResult OnPostResetFilter()
    //{
    //    return RedirectToAction("Index");
    //}

}
