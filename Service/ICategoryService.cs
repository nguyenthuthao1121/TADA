using TADA.Dto.Category;

namespace TADA.Service;

public interface ICategoryService
{
    List<CategoryDto> GetAllCategories();
    //tra ve true neu add thanh cong, false neu add that bai
    bool AddCategory(string categoryName);
}
