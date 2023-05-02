using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Category;
using TADA.Dto.Provider;
using TADA.Middleware;
using TADA.Model.Entity;
using TADA.Service;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class BookManagementModel : PageModel
{
    private readonly IBookService bookService;
    private readonly ICategoryService categoryService;
    private readonly IProviderService providerService;
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
        Books = bookService.GetBooksForManagement(CategoryId, ProviderId, q, InStock, SortBy, SortType);
        Categories = categoryService.GetAllCategories();
        Providers = providerService.GetAllProviders();

    }
}
