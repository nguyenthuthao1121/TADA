using TADA.Dto;

namespace TADA.Service;

public interface IBookService
{
    public List<BookDto> GetAllBook();

    public List<BookDto> GetBooks(int category, String priceRange, String sortBy);
}
