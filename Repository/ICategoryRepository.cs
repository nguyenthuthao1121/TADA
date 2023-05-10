using TADA.Model.Entity;

namespace TADA.Repository;

public interface ICategoryRepository
{
    List<Category> GetAllCategories();
    List<Category> GetCategories(string search, string sortBy, string sortType);
    List<Category> GetAllCategoriesOrderByName();
    string GetCategoryNameById(int id);
    Category GetCategoryByName(string categoryName);
    void AddCategory(string categoryName);
}
