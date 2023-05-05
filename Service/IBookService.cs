using TADA.Dto.Book;

namespace TADA.Service;

public interface IBookService
{
    List<BookDto> GetAllBooks();
    List<BookDto> GetBooks(int category, string? search, string priceRange, string genre, string sortBy);
    List<BookDto> SearchBooks(string query);
    List<BookManagementDto> GetAllBooksForManagement();
    BookDto GetBookById(int id);
    List<SoldBookDto> GetSoldBooks();
}
