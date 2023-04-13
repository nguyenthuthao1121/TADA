using TADA.Dto.Category;

namespace TADA.Service;

public interface ICategoryService
{
    List<CategoryDto> GetAllCategories();
}
