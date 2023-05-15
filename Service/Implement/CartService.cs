using Microsoft.Identity.Client;
using System.Net;
using TADA.Dto.Book;
using TADA.Dto.Cart;
using TADA.Dto.Order;
using TADA.Model;
using TADA.Model.Entity;
using TADA.Repository;
using TADA.Repository.Implement;

namespace TADA.Service.Implement
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly IOrderRepository orderRepository;
        public CartService(ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            this.cartRepository = cartRepository;
            this.orderRepository = orderRepository;
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
        public void DeleteBookOfOrder(int orderId, int accountId)
        {
            var orderDetails= orderRepository.GetOrderDetailsByOrderId(orderId);
            foreach(var item in orderDetails)
            {
                cartRepository.DeleteBookOfCart(item.BookId, accountId);
            }
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
        public void UpdateQuantityOfCartDetail(int accountId, int bookId, int quantity)
        {
            cartRepository.UpdateQuantityOfCartDetail(accountId, bookId, quantity);
        }
        public void AddCart(int customerId)
        {
            cartRepository.AddCart(customerId);
        }
    }
}
