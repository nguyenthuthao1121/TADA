using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using TADA.Dto;
using TADA.Dto.Book;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IBookRepository
{
    List<BookDto> GetAllBooks();
    List<BookDto> GetBooks(int category, string priceRange, string genre, string sortBy);
    List<BookDto> GetBooksForManagement(int category, int provider, int inStock, string sortBy, string sortType);
    List<BookDto> SearchBooks(string query);
    BookDto GetBookById(int id);
    int GetNumOfBooksByCategoryId(int categoryId);
    List<SoldBookDto> GetSoldBooks();
    BookDto GetBookByISBN(string isbn);
    int AddBook(BookDto book);
    void UpdateQuantity(int bookId, int quantity);
    void UpdateBook(BookDto book);
    void HideBook(int bookId);
    void DisplayBook(int bookId);

}
