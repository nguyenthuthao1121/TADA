using TADA.Dto;
using TADA.Repository;

namespace TADA.Service.Implement;

public class BookService : IBookService
{
    private readonly IBookRepository bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }
    public List<BookDto> GetAllBook()
    {
        var books = bookRepository.GetAllBooks();
        var listBook = new List<BookDto>();
        foreach (var book in books)
        {
            listBook.Add(new BookDto()
            {
                Id = book.Id,
                Image = book.Image,
                Name = book.Name,
                Price = book.Price,
                Promotion = book.Promotion,
            });
        }
        return listBook;
    }

    public List<BookDto> GetBooks(int category, string priceRange, string sortBy)
    {
        var books = bookRepository.GetBooks(category, priceRange, sortBy);
        var listBook = new List<BookDto>();
        foreach (var book in books)
        {
            listBook.Add(new BookDto()
            {
                Id = book.Id,
                Image = book.Image,
                Name = book.Name,
                Price = book.Price,
                Promotion = book.Promotion,
            });
        }
        return listBook;
    }
}
