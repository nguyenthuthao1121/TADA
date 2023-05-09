using TADA.Dto.Category;
using TADA.Model.Entity;
using TADA.Repository;

namespace TADA.Service.Implement;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IBookRepository bookRepository;

    public CategoryService(ICategoryRepository categoryRepository, IBookRepository bookRepository)
    {
        this.categoryRepository = categoryRepository;
        this.bookRepository = bookRepository;
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
                Name = category.Name,
                NumOfBooks = bookRepository.GetNumOfBooksByCategoryId(category.Id)
            });
        }
        return listCategories;
    }
    public List<CategoryDto> GetAllCategoriesOrderByName()
    {
        var categories = categoryRepository.GetAllCategoriesOrderByName();
        var listCategories = new List<CategoryDto>();
        foreach (var category in categories)
        {
            listCategories.Add(new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                NumOfBooks = bookRepository.GetNumOfBooksByCategoryId(category.Id)
            });
        }
        return listCategories;
    }
    public bool AddCategory(string categoryName)
    {
        if (categoryRepository.GetCategoryByName(categoryName) == null)
        {
            categoryRepository.AddCategory(categoryName);
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<CategoryDto> GetCategories(string search, string sortBy, string sortType)
    {
        var categories = categoryRepository.GetCategories(search, sortBy, sortType);
        var listCategories = new List<CategoryDto>();
        foreach (var category in categories)
        {
            listCategories.Add(new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                NumOfBooks = bookRepository.GetNumOfBooksByCategoryId(category.Id)
            });
        }
        return listCategories;
    }
}
