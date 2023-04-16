using TADA.Dto.Book;
using TADA.Repository;

namespace TADA.Service.Implement;

public class BookService : IBookService
{
    private readonly IBookRepository bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
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
}
