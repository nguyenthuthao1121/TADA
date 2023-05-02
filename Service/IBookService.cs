using TADA.Dto.Book;

namespace TADA.Service;

public interface IBookService
{
    List<BookDto> GetAllBooks();
    List<BookDto> GetBooks(int category, string search, string priceRange, string genre, string sortBy);
    List<BookDto> SearchBooks(string query);
    List<BookManagementDto> GetAllBooksForManagement();
    List<BookManagementDto> GetBooksForManagement(int category, int provider, string search, int inStock, string sortBy, string sortType);
    void UpdateBook(BookDto book);
    BookDto GetBookById(int id);
}
