using TADA.Dto.Book;
using TADA.Dto.Cart;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface ICartRepository
{
    CartDto GetCartByCustomerId(int customerId);
    List<CartDetailDto> GetCartDetailsByCustomerId(int customerId);
    CartDto GetCartByAccountId(int accountId);
    List<CartDetailDto> GetCartDetailsByAccountId(int accountId);
    CartDetailDto GetCartDetail(int accountId, int bookId);
    BookDto GetBookByCartDetail(CartDetailDto cartDetail);
    List<BookDto> GetBooksOfCart(int accountId);
    void AddBookToCart(int bookId, int cartId, int quantity);
    void DeleteBookOfCart(int bookId, int accountId);
    void UpdateQuantityOfCartDetail(int accountId, int bookId, int quantity);
    void AddCart(int customerId);
}
