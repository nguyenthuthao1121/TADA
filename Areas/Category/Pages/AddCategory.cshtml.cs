using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TADA.Service;
using TADA.Middleware;

namespace TADA.Pages;

[Authorize("Quản trị viên", "Nhân viên bán hàng")]
public class AddCategoryModel : PageModel
{
    private readonly ICategoryService categoryService;
    [BindProperty]
    [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
    public string CategoryName { get; set; }
    public string Message { get; set; }
    public AddCategoryModel(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    public IActionResult OnPost()
    {
        if (CategoryName != null)
        {
            if (categoryService.AddCategory(CategoryName))
            {

                return RedirectToPage("/CategoryManagement", new { area = "Category" });
            }
            else
            {
                Message = "Danh mục đã tồn tại";
                return Page();
            }
        }
        else return Page();
    }
}
