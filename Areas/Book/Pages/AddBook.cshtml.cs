using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Book;
using TADA.Dto.Category;
using TADA.Dto.Provider;
using TADA.Service;

namespace TADA.Pages;

public class AddBookModel : PageModel
{
    private readonly IBookService bookService;
    private readonly IProviderService providerService;
    private readonly ICategoryService categoryService;

    public AddBookModel(IBookService bookService, IProviderService providerService, ICategoryService categoryService)
    {
        this.bookService = bookService;
        this.providerService = providerService;
        this.categoryService = categoryService;
    }
    [BindProperty]
    public BookDto Book { get; set; }
    [BindProperty]
    public string DescriptionText { get; set; }
    [BindProperty]
    public string ImagePath { get; set; }
    public List<ProviderManagementDto> Providers { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public void OnGet()
    {
        Providers = providerService.GetAllProviders();
        Categories= categoryService.GetAllCategories();
    }
    
    public IActionResult OnPostAddNewBook()
    {
        
        return RedirectToPage("BookManagement");
    }
}
