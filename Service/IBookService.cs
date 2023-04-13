using TADA.Dto;

namespace TADA.Service;

public interface IBookService
{
    List<BookDto> GetAllBook();

    List<BookDto> GetBooks(int category, String priceRange, String sortBy);

    BookDto GetBookById(int id);
}
