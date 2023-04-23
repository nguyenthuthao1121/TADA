using TADA.Dto.Book;
using TADA.Dto.Cart;

namespace TADA.Service;

public interface ICartService
{
    CartDto GetCartByCustomerId(int customerId);
    List<CartDetailDto> GetCartDetailsByCustomerId(int customerId);
    CartDto GetCartByAccountId(int accountId);
    List<CartDetailDto> GetCartDetailsByAccountId(int accountId);
    BookDto GetBookByCartDetail(CartDetailDto cartDetail);
    void AddBookToCart(int bookId, int accountId, int quantity);
    void DeleteBookOfCart(int bookId, int accountId);
    void IncreaseQuantityOfCartDetail(int accountId, int bookId, int delta);
    void DecreaseQuantityOfCartDetail(int accountId, int bookId, int delta);
}
