using TADA.Model.Entity;

namespace TADA.Repository;

public interface ICategoryRepository
{
    List<Category> GetAllCategories();
    string GetCategoryNameById(int id);
    Category GetCategoryByName(string categoryName);
    void AddCategory(string categoryName);
}
