using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Category;
using TADA.Dto.Provider;
using TADA.Middleware;
using TADA.Model.Entity;
using TADA.Service;
using static System.Reflection.Metadata.BlobBuilder;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class BookManagementModel : PageModel
{
    private readonly IBookService bookService;
    private readonly ICategoryService categoryService;
    private readonly IProviderService providerService;

    public const int ITEMS_PER_PAGE = 10;
    public int countPages { get; set; }
    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int currentPage { get; set; }
    public List<BookManagementDto> Books { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public List<ProviderManagementDto> Providers { get; set; }
    [BindProperty(SupportsGet = true)]
    public int ProviderId { get; set; }
    [BindProperty(SupportsGet = true)]


    public int CategoryId { get; set; }
    [BindProperty(SupportsGet = true)]


    public int InStock { get; set; }

    [BindProperty(SupportsGet = true)]
    public string q { get; set; }
    [BindProperty(SupportsGet = true)]

    public string SortBy { get; set; }
    [BindProperty(SupportsGet = true)]

    public string SortType { get; set; }


    public BookManagementModel(IBookService bookService, ICategoryService categoryService, IProviderService providerService)
    {
        this.bookService = bookService;
        this.categoryService = categoryService;
        this.providerService = providerService;
    }

    public void OnGet()
    {
        var books = bookService.GetBooksForManagement(CategoryId, ProviderId, q, InStock, SortBy, SortType);
        Categories = categoryService.GetAllCategories();
        Providers = providerService.GetAllProviders();
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
