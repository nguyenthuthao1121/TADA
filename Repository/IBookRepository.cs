using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using TADA.Dto;
using TADA.Dto.Book;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IBookRepository
{
    List<BookDto> GetAllBooks();
    List<BookDto> GetBooks(int category, string search, string priceRange, string genre, string sortBy);
    List<BookDto> GetBooksForManagement(int category, int provider, string? search, int inStock, string sortBy, string sortType);
    List<BookDto> SearchBooks(string query);
    BookDto GetBookById(int id);
    int GetNumOfBooksByCategoryId(int categoryId);
    void UpdateBook(BookDto book);
}
