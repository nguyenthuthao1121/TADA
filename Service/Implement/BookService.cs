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
    public List<BookDto> GetBooks(int category, string priceRange, string sortBy)
    {
        return bookRepository.GetBooks(category, priceRange, sortBy);
          
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
                Provider = providerRepository.GetProviderNameById(book.ProviderId)
            });
        }
        return list;
    }
}
