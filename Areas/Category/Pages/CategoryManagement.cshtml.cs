using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TADA.Dto.Category;
using TADA.Middleware;
using TADA.Service;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class CategoryManagementModel : PageModel
{
    private readonly ICategoryService categoryService;
    public List<CategoryDto> Categories { get; set; }
    public const int ITEMS_PER_PAGE = 10;
    public int countPages { get; set; }
    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int currentPage { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SearchQuery { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortType { get; set; }

    public CategoryManagementModel(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    public void OnGet()
    {
        var categories = categoryService.GetCategories(SearchQuery, SortBy, SortType);
        int total = categories.Count();
        countPages = (int)Math.Ceiling((double)total / ITEMS_PER_PAGE);
        if (currentPage < 1)
        {
            currentPage = 1;
        }
        if (currentPage > countPages)
        {
            currentPage = countPages;
        }
        Categories = categories.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
    }
}
