using TADA.Model.Entity;
using TADA.Model;
using Microsoft.DotNet.Scaffolding.Shared;

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
    public List<Category> GetAllCategoriesOrderByName()
    {
        return context.Categories.OrderBy(category=>category.Name).ToList();
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

    public List<Category> GetCategories(string sortBy, string sortType)
    {
        var categories = context.Categories.ToList();
        foreach(var category in categories)
        {
            var entry = context.Entry(category);
            entry.Collection(p => p.Books).Load();
        }
        switch (sortType)
        {
            case "desc":
                switch (sortBy)
                {
                    case "categoryName":
                        categories = categories.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "count":
                        categories = categories.OrderByDescending(p => p.Books.Count).ToList();
                        break;
                    default:
                        categories = categories.OrderByDescending(p => p.Id).ToList();
                        break;
                }
                break;
            default:
                switch (sortBy)
                {
                    case "categoryName":
                        categories = categories.OrderBy(p => p.Name).ToList();
                        break;
                    case "count":
                        categories = categories.OrderBy(p => p.Books.Count).ToList();
                        break;
                    default:
                        categories = categories.OrderBy(p => p.Id).ToList();
                        break;
                }
                break;
        }
        return categories;
    }
}
