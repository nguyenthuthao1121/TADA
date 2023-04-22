using TADA.Model.Entity;
using TADA.Model;

namespace TADA.Repository.Implement;

public class CategoryRepository : ICategoryRepository
{
    private readonly TadaContext context;
    public CategoryRepository(TadaContext context)
    {
        this.context = context;
    }
    public List<Category> GetAllCategories()
    {
        return context.Categories.ToList();
    }
    public string GetCategoryNameById(int id)
    {
        return context.Categories.Where(category => category.Id == id).Select(category => category.Name).FirstOrDefault();
    }
    public Category GetCategoryByName(string categoryName) 
    { 
        return context.Categories.Where(category => category.Name == categoryName).FirstOrDefault();
    }
    public void AddCategory(string categoryName)
    {
        context.Categories.Add(new Category { Name = categoryName });
        context.SaveChanges();
    }
}
