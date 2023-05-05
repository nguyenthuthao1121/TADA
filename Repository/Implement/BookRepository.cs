using System.Data.Entity;
using TADA.Dto;
using TADA.Dto.Book;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement;

public class BookRepository : IBookRepository
{
    private readonly TadaContext context;
    private readonly IOrderRepository orderRepository;
    public BookRepository(TadaContext context, IOrderRepository orderRepository)
    {
        this.context = context;
        this.orderRepository = orderRepository;
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

    public List<BookDto> GetBooks(int category, string? search, string priceRange, string genre, string sortBy)
    {
        if(genre.Equals("All"))
        {
            genre = "";
        }
        int min = 0;
        int max = int.MaxValue;
        var i = priceRange.Split(",");
        try
        {
            min = int.Parse(i[0]);
        }
        catch (Exception)
        {
            min = 0;
        }
        try
        {
            max = int.Parse(i[1]);
        }
        catch (Exception)
        {
            max = int.MaxValue;
        }
        List<int> bookIds = new List<int>();
        List<BookDto> books = new List<BookDto>();
        if (string.IsNullOrWhiteSpace(search))
        {
            if (category != 0)
            {
                switch (sortBy)
                {
                    case "Asc":
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).OrderBy(x => x.Price).Select(p => p.Id).ToList();
                        break;
                    case "Desc":
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).OrderByDescending(x => x.Price).Select(p => p.Id).ToList();
                        break;
                    default:
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).OrderByDescending(x => x.Id).Select(p => p.Id).ToList();
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "Asc":
                        bookIds = context.Books.OrderBy(x => x.Price).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).Select(p => p.Id).ToList();
                        break;
                    case "Desc":
                        bookIds = context.Books.OrderByDescending(x => x.Price).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).Select(p => p.Id).ToList();
                        break;
                    default:
                        bookIds = context.Books.OrderByDescending(x => x.Id).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre)).Select(p => p.Id).ToList();
                        break;
                }
            }
        }
        else
        {
            if (category != 0)
            {
                switch (sortBy)
                {
                    case "Asc":
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).OrderBy(x => x.Price).Select(p => p.Id).ToList();
                        break;
                    case "Desc":
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).OrderByDescending(x => x.Price).Select(p => p.Id).ToList();
                        break;
                    default:
                        bookIds = context.Books.Where(x => x.CategoryId == category).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).OrderByDescending(x => x.Id).Select(p => p.Id).ToList();
                        break;
                }
            }
            else
            {
                switch (sortBy)
                {
                    case "Asc":
                        bookIds = context.Books.OrderBy(x => x.Price).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).Select(p => p.Id).ToList();
                        break;
                    case "Desc":
                        bookIds = context.Books.OrderByDescending(x => x.Price).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).Select(p => p.Id).ToList();
                        break;
                    default:
                        bookIds = context.Books.OrderByDescending(x => x.Id).Where(x => x.Price <= max && x.Price >= min && x.Genre.Contains(genre) && x.Name.Contains(search)).Select(p => p.Id).ToList();
                        break;
                }
            }
        }
        foreach(int id in bookIds)
        {
            books.Add(GetBookById(id));
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
            ProviderId = book.ProviderId,
            ProviderName = book.Provider.Name
		};
    }
    public int GetNumOfBooksByCategoryId(int categoryId)
    {
        return context.Books.Where(book => book.CategoryId == categoryId).Count();
    }

    public List<BookDto> SearchBooks(string query)
    {
        List<int> bookIds = context.Books.Where(p => p.Name.Contains(query)).Select(p => p.Id).ToList();
        List<BookDto> bookDtos = new List<BookDto>();
        foreach (int id in bookIds)
        {
            bookDtos.Add(GetBookById(id));
        }
        return bookDtos;
    }
    public List<SoldBookDto> GetSoldBooks()
    {
        var orderGroups = orderRepository.GetOrderGroupByBookId();
        return context.Books.ToList().Join(orderGroups, book => book.Id, orderGroup => orderGroup.BookId, (book, orderGroup) => new SoldBookDto
        {
            BookId = book.Id,
            Name = book.Name,
            Image = book.Image,
            SoldQuantity = orderGroup.Quantity
        }).OrderByDescending(soldBook => soldBook.SoldQuantity).ToList();
    }
}
