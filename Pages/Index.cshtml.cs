using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto;
using TADA.Model;
using TADA.Service;

namespace TADA.Pages;

public class IndexModel : PageModel
{
    private readonly IBookService bookService;
    private readonly ICategoryService categoryService;

    public List<BookDto> Books { get; set; }
    public List<CategoryDto> Categories { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Category { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "New";
    [BindProperty(SupportsGet = true)]
    public string PriceRange { get; set; } = string.Empty;


    public IndexModel(IBookService bookService, ICategoryService categoryService)
    {
        this.bookService = bookService;
        this.categoryService = categoryService;
    }

    public void OnGet()
    {
        Books = bookService.GetBooks(Category, PriceRange, SortBy);
        Categories = categoryService.GetAllCategories();
    }


}
