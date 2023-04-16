﻿using TADA.Dto.Book;
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

        public BookDto GetBookByCartDetail(CartDetailDto cartDetail)
        {
            return cartRepository.GetBookByCartDetail(cartDetail);
        }
    }
}