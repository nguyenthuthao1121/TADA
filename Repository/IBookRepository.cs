using TADA.Dto;
using TADA.Dto.Book;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IBookRepository
{
<<<<<<< HEAD
    List<Book> GetAllBooks();
    List<Book> GetBooks(int category, string priceRange, string sortBy);
    //List<BookManagementDto> GetAllBooksForManagement();
=======
    List<BookDto> GetAllBooks();
    List<BookDto> GetBooks(int category, string priceRange, string sortBy);
>>>>>>> c62899945b3ed94c449ef38cc7ef364fac3db29e
    BookDto GetBookById(int id);
    int GetNumOfBooksByCategoryId(int categoryId);
}
