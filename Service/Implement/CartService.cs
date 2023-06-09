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
            try
            {
                return cartRepository.GetCartByCustomerId(customerId);
            }
            catch (Exception)
            {
                return new CartDto();
            }
        }

        public CartDto GetCartByAccountId(int accountId)
        {
            try
            {
                return cartRepository.GetCartByAccountId(accountId);
            }
            catch (Exception)
            {
                return new CartDto();
            }
            
        }

        public List<CartDetailDto> GetCartDetailsByCustomerId(int customerId)
        {
            try
            {
                return cartRepository.GetCartDetailsByCustomerId(customerId);
            }
            catch (Exception)
            {
                return new List<CartDetailDto>();
            }
            
        }

        public List<CartDetailDto> GetCartDetailsByAccountId(int accountId)
        {
            try
            {
                return cartRepository.GetCartDetailsByAccountId(accountId);
            }
            catch (Exception)
            {
                return new List<CartDetailDto>();
            }
            
        }
        public CartDetailDto GetCartDetail(int accountId, int bookId)
        {
            try
            {
                return cartRepository.GetCartDetail(accountId, bookId);
            }
            catch (Exception)
            {
                return new CartDetailDto();
            }
            
        }
        public BookDto GetBookByCartDetail(CartDetailDto cartDetail)
        {
            try
            {
                return cartRepository.GetBookByCartDetail(cartDetail);
            }
            catch (Exception)
            {
                return new BookDto();
            }
        }
        public List<BookDto> GetBooksOfCart(int accountId)
        {
            try
            {
                return cartRepository.GetBooksOfCart(accountId);
            }
            catch (Exception)
            {
                return new List<BookDto>();
            }
        }

        public void AddBookToCart(int bookId, int accountId, int quantity)
        {
            try
            {
                var cart = GetCartByAccountId(accountId);
                cartRepository.AddBookToCart(bookId, cart.Id, quantity);
            }
            catch (Exception)
            {
            }
            
        }
        public void DeleteBookOfCart(int bookId, int accountId)
        {
            try
            {
                cartRepository.DeleteBookOfCart(bookId, accountId);
            }
            catch (Exception)
            {
            }
            
        }
        public void DeleteBookOfOrder(int orderId, int accountId)
        {
            try
            {
                var orderDetails = orderRepository.GetOrderDetailsByOrderId(orderId);
                foreach (var item in orderDetails)
                {
                    cartRepository.DeleteBookOfCart(item.BookId, accountId);
                }
            }
            catch (Exception)
            {
            }
            
        }
        public void IncreaseQuantityOfCartDetail(int accountId, int bookId, int delta)
        {
            try
            {
                var cartDetail = cartRepository.GetCartDetail(accountId, bookId);
                cartRepository.UpdateQuantityOfCartDetail(accountId, bookId, cartDetail.Quantity + delta);
            }
            catch (Exception)
            {
            }
            
        }
        public void DecreaseQuantityOfCartDetail(int accountId, int bookId, int delta)
        {
            try
            {
                var cartDetail = cartRepository.GetCartDetail(accountId, bookId);
                cartRepository.UpdateQuantityOfCartDetail(accountId, bookId, cartDetail.Quantity - delta);
            }
            catch (Exception)
            {
            }
            
        }
        public void UpdateQuantityOfCartDetail(int accountId, int bookId, int quantity)
        {
            try
            {
                cartRepository.UpdateQuantityOfCartDetail(accountId, bookId, quantity);
            }
            catch (Exception)
            {
            }
            
        }
        public void AddCart(int customerId)
        {
            try
            {
                cartRepository.AddCart(customerId);
            }
            catch (Exception)
            {
            }
            
        }
    }
}
