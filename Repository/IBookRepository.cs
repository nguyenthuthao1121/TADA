using TADA.Dto;
using TADA.Dto.Book;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IBookRepository
{
    List<BookDto> GetAllBooks();
    List<BookDto> GetBooks(int category, string priceRange, string sortBy);
    BookDto GetBookById(int id);
}
