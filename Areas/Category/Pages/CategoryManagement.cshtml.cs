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
    public CategoryManagementModel(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    public void OnGet()
    {
        Categories = categoryService.GetAllCategories();
    }
}
