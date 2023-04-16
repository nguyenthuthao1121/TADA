﻿using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using TADA.Dto.Book;
using TADA.Dto.Cart;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement;

public class CartRepository : ICartRepository
{
    private readonly TadaContext context;
    public int LastId { get; set; }
    public CartRepository(TadaContext context)
    {
        this.context = context;
    }

    public CartDto GetCartByCustomerId(int customerId)
    {
        return context.Carts
            .Where(cart=>cart.CustomerId == customerId)
            .Select(cart=>new CartDto(cart.Id, cart.CustomerId)).FirstOrDefault();
    }
    public List<CartDetailDto> GetCartDetailsByCustomerId(int customerId)
    {
        List<CartDetailDto> cartDetailDtos = new List<CartDetailDto>();
        List<CartDetail> cartDetails = context.Carts
            .Where(cart => cart.CustomerId == customerId)
            .Select(cart => cart.CartDetails).FirstOrDefault().ToList();
        foreach (CartDetail cartDetail in cartDetails)
        {
            cartDetailDtos.Add(new CartDetailDto(cartDetail));
        }
        return cartDetailDtos;
    }

    public CartDto GetCartByAccountId(int accountId)
    {
        var customerId= context.Customers.Where(customer => customer.AccountId == accountId).Select(customer => customer.Id).FirstOrDefault();
        return GetCartByCustomerId(customerId);
    }

    public List<CartDetailDto> GetCartDetailsByAccountId(int accountId)
    {
        var customerId = context.Customers.Where(customer => customer.AccountId == accountId).Select(customer => customer.Id).FirstOrDefault();
        return GetCartDetailsByCustomerId(customerId);
    }

    public BookDto GetBookByCartDetail(CartDetailDto cartDetail)
    {
        var book = context.Books.Find(cartDetail.BookId);
        return new BookDto
        {
            Id = book.Id,
            Name= book.Name,
            Author= book.Author,
            Publisher= book.Publisher,
            PublicationYear= book.PublicationYear,
            Genre= book.Genre,
            Pages= book.Pages,
            Length= book.Length,
            Weight= book.Weight,
            Width= book.Width,
            Price= book.Price,
            Cover= book.Cover,
            Quantity= book.Quantity,
            Description= book.Description,
            Image= book.Image,
            Promotion= book.Promotion,
            CategoryId= book.CategoryId,
        };
    }

}