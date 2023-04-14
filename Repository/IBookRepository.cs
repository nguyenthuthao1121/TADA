using TADA.Dto;
using TADA.Dto.BookDto;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IBookRepository
{
    List<Book> GetAllBooks();

    List<Book> GetBooks(int category, string priceRange, string sortBy);
    BookDto GetBookById(int id);
}
