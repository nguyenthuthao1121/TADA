using TADA.Dto.Book;

namespace TADA.Service;

public interface IBookService
{
    List<BookDto> GetAllBooks();
    List<BookDto> GetBooks(int category, string search, string priceRange, string genre, string sortBy);
    List<BookDto> SearchBooks(string query);
    List<BookManagementDto> GetAllBooksForManagement();
    List<BookManagementDto> GetBooksForManagement(int category, int provider, string search, int inStock, string sortBy, string sortType);
    BookDto GetBookByISBN(string isbn);
    int AddBook(BookDto book);
    void UpdateQuantity(int bookId, int quantity);
    void UpdateBook(BookDto book);
    void HideBook(int bookId);
    void DisplayBook(int bookId);

    BookDto GetBookById(int id);
    List<SoldBookDto> GetSoldBooks();
}
