using TADA.Dto.Book;
using TADA.Dto.Cart;
using TADA.Model;
using TADA.Model.Entity;
using TADA.Repository;
using TADA.Repository.Implement;

namespace TADA.Service.Implement
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public CartDto GetCartByCustomerId(int customerId)
        {
            return cartRepository.GetCartByCustomerId(customerId);
        }

        public CartDto GetCartByAccountId(int accountId)
        {
            return cartRepository.GetCartByAccountId(accountId);
        }

        public List<CartDetailDto> GetCartDetailsByCustomerId(int customerId)
        {
            return cartRepository.GetCartDetailsByCustomerId(customerId);
        }

        public List<CartDetailDto> GetCartDetailsByAccountId(int accountId)
        {
            return cartRepository.GetCartDetailsByAccountId(accountId);
        }
        public CartDetailDto GetCartDetail(int accountId, int bookId)
        {
            return cartRepository.GetCartDetail(accountId, bookId);
        }
        public BookDto GetBookByCartDetail(CartDetailDto cartDetail)
        {
            return cartRepository.GetBookByCartDetail(cartDetail);
        }
        public List<BookDto> GetBooksOfCart(int accountId)
        {
            return cartRepository.GetBooksOfCart(accountId);
        }

        public void AddBookToCart(int bookId, int accountId, int quantity)
        {
            var cart=GetCartByAccountId(accountId);
            cartRepository.AddBookToCart(bookId,cart.Id, quantity);
        }
        public void DeleteBookOfCart(int bookId, int accountId)
        {
            cartRepository.DeleteBookOfCart(bookId,accountId);
        }
        public void IncreaseQuantityOfCartDetail(int accountId, int bookId, int delta)
        {
            var cartDetail=cartRepository.GetCartDetail(accountId, bookId);
            cartRepository.UpdateQuantityOfCartDetail(accountId, bookId, cartDetail.Quantity + delta);
        }
        public void DecreaseQuantityOfCartDetail(int accountId, int bookId, int delta)
        {
            var cartDetail = cartRepository.GetCartDetail(accountId, bookId);
            cartRepository.UpdateQuantityOfCartDetail(accountId, bookId, cartDetail.Quantity - delta);
        }
    }
}
