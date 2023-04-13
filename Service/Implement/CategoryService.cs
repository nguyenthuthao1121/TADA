using TADA.Dto.Category;
using TADA.Repository;

namespace TADA.Service.Implement;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }
    public List<CategoryDto> GetAllCategories()
    {
        var categories = categoryRepository.GetAllCategories();
        var listCategories = new List<CategoryDto>();
        foreach (var category in categories)
        {
            listCategories.Add(new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name
            });
        }
        return listCategories;
    }
}
