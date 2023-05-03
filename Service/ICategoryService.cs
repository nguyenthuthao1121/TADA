using TADA.Dto.Category;
using TADA.Model.Entity;

namespace TADA.Service;

public interface ICategoryService
{
    List<CategoryDto> GetAllCategories();
    List<CategoryDto> GetAllCategoriesOrderByName();
    //tra ve true neu add thanh cong, false neu add that bai
    bool AddCategory(string categoryName);
}
