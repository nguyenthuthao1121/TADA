using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement;

public class BookRepository : IBookRepository
{
    private readonly TadaContext context;
    public BookRepository(TadaContext context)
    {
        this.context = context;
    }
    public List<Book> GetAllBooks()
    {
        return context.Books.ToList();
    }

    public List<Book> GetBooks(int category, string priceRange, string sortBy)
    {
        int min = 0;
        int max = int.MaxValue;
        try
        {
            var i = priceRange.Replace(" ", "").Split("-");
            min = int.Parse(i[0]);
            max = int.Parse(i[1]);
        }catch(Exception)
        {
            min = 0;
            max = int.MaxValue;
        }
        
        if (category != 0)
        {
            switch (sortBy)
            {
                case "Asc": return context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min).OrderBy(x => x.Price).ToList();
                case "Desc": return context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min).OrderByDescending(x => x.Price).ToList();
                default: return context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min).OrderByDescending(x => x.Id).ToList();
            }
        }
        else
        {
            switch (sortBy)
            {
                case "Asc": return context.Books.OrderBy(x => x.Price).Where(x => x.Price <= max && x.Price >= min).ToList();
                case "Desc": return context.Books.OrderByDescending(x => x.Price).Where(x => x.Price <= max && x.Price >= min).ToList();
                default: return context.Books.OrderByDescending(x => x.Id).Where(x => x.Price <= max && x.Price >= min).ToList();
            }
        }
    }
}
