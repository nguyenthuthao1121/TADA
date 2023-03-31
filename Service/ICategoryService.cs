using TADA.Dto;

namespace TADA.Service;

public interface ICategoryService
{
    List<CategoryDto> GetAllCategories();
}
