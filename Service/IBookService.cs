using TADA.Dto.Book;

namespace TADA.Service;

public interface IBookService
{
    List<BookDto> GetAllBooks();

    List<BookDto> GetBooks(int category, String priceRange, String sortBy);
    List<BookDto> SearchBooks(string query);
    List<BookManagementDto> GetAllBooksForManagement();

    BookDto GetBookById(int id);
}
