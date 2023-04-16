using TADA.Dto;
using TADA.Dto.Book;
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
    public List<BookDto> GetAllBooks()
    {
        var bookIds = context.Books.Select(p => p.Id).ToList();
        List<BookDto> books = new List<BookDto>();
        foreach(var book in bookIds)
        {
            books.Add(GetBookById(book));
        }
        return books;
    }

    public List<BookDto> GetBooks(int category, string priceRange, string sortBy)
    {
        int min = 0;
        int max = int.MaxValue;
        try
        {
            var i = priceRange.Replace(" ", "").Split("-");
            min = int.Parse(i[0]);
            max = int.Parse(i[1]);
        }
        catch (Exception)
        {
            min = 0;
            max = int.MaxValue;
        }
        List<int> bookIds = new List<int>();
        List<BookDto> books = new List<BookDto>();
        if (category != 0)
        {
            switch (sortBy)
            {
                case "Asc": 
                    bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min).OrderBy(x => x.Price).Select(p => p.Id).ToList();
                    break;
                case "Desc":
                    bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min).OrderByDescending(x => x.Price).Select(p => p.Id).ToList();
                    break;
                default:
                    bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min).OrderByDescending(x => x.Id).Select(p => p.Id).ToList();
                    break;
            }
        }
        else
        {
            switch (sortBy)
            {
                case "Asc":
                    bookIds = context.Books.OrderBy(x => x.Price).Where(x => x.Price <= max && x.Price >= min).Select(p => p.Id).ToList();
                    break;
                case "Desc":
                    bookIds = context.Books.OrderByDescending(x => x.Price).Where(x => x.Price <= max && x.Price >= min).Select(p => p.Id).ToList();
                    break;
                default:
                    bookIds = context.Books.OrderByDescending(x => x.Id).Where(x => x.Price <= max && x.Price >= min).Select(p => p.Id).ToList();
                    break;
            }
        }
        foreach(int i in bookIds)
        {
            books.Add(GetBookById(i));
        }
        return books;
    }
    public BookDto GetBookById(int bookId)
    {
        var book = context.Books.FirstOrDefault(x => x.Id == bookId);
        var entry = context.Entry(book);
        entry.Reference(b => b.Category).Load();
        entry.Reference(b => b.Provider).Load();
        entry.Collection(b => b.Reviews).Load();
        double rating = (book.Reviews.Count() == 0) ? 0 : book.Reviews.Average(x => x.Rating);
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Author = book.Author,
            Publisher = book.Publisher,
            PublicationYear = book.PublicationYear,
            Genre = book.Genre,
            Pages = book.Pages,
            Length = book.Length,
            Width = book.Width,
            Weight = book.Weight,
            Price = book.Price,
            Cover = book.Cover,
            Quantity = book.Quantity,
            Description = book.Description,
            Image = book.Image,
            Promotion = book.Promotion,
            AverageRating = rating,
            NumberOfReview = book.Reviews.Count(),
            CategoryId = book.CategoryId,
            CategoryName = book.Category.Name,
            ProviderName = book.Provider.Name
		};
    }
    public int GetNumOfBooksByCategoryId(int categoryId)
    {
        return context.Books.Where(book => book.CategoryId == categoryId).Count();
    }

}
