using TADA.Model.Entity;

namespace TADA.Repository;

public interface ICategoryRepository
{
    List<Category> GetAllCategories();
}
