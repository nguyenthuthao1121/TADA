using TADA.Dto;
using TADA.Dto.Book;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IBookRepository
{
    List<Book> GetAllBooks();
    List<Book> GetBooks(int category, string priceRange, string sortBy);
    //List<BookManagementDto> GetAllBooksForManagement();
    BookDto GetBookById(int id);
    int GetNumOfBooksByCategoryId(int categoryId);
}
