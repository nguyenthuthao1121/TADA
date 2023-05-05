using TADA.Dto.Book;
using TADA.Repository;

namespace TADA.Service.Implement;

public class BookService : IBookService
{
    private readonly IBookRepository bookRepository;
    private readonly ICategoryRepository categoryRepository;
    private readonly IProviderRepository providerRepository;
    public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IProviderRepository providerRepository)
    {
        this.bookRepository = bookRepository;
        this.categoryRepository = categoryRepository;
        this.providerRepository = providerRepository;
    }
    public List<BookDto> GetAllBooks()
    {
        return bookRepository.GetAllBooks();
    }
    public List<BookDto> GetBooks(int category, string? search, string priceRange, string genre, string sortBy)

    {
        return bookRepository.GetBooks(category, search, priceRange, genre, sortBy);
          
    }

    public BookDto GetBookById(int id)
    {
        return bookRepository.GetBookById(id);
    }
    public List<BookManagementDto> GetAllBooksForManagement()
    {
        List<BookManagementDto> list = new List<BookManagementDto>();
        var books = bookRepository.GetAllBooks();
        foreach(var book in books)
        {
            list.Add(new BookManagementDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Publisher = book.Publisher,
                Price = book.Price,
                Promotion = book.Promotion,
                Quantity = book.Quantity,
                Image = book.Image,
                Category = categoryRepository.GetCategoryNameById(book.CategoryId),
                Provider = book.ProviderName
            });
        }
        return list;
    }

    public List<BookDto> SearchBooks(string query)
    {
        return bookRepository.SearchBooks(query);
    }
    public List<SoldBookDto> GetSoldBooks()
    {
        return bookRepository.GetSoldBooks();
    }
}
