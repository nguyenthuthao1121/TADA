using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using TADA.Dto.Book;
using TADA.Dto.Cart;
using TADA.Dto.Order;
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
        var cart= context.Carts
            .Where(cart => cart.CustomerId == customerId)
            .Select(cart => new CartDto(cart.Id, cart.CustomerId)).FirstOrDefault();
        List<CartDetail> cartDetails = context.CartDetail
            .Where(cartDetails=>cartDetails.CartId==cart.Id).ToList();
        foreach (CartDetail cartDetail in cartDetails)
        {
            cartDetailDtos.Add(new CartDetailDto
            {
                BookId= cartDetail.BookId,
                CartId= cartDetail.CartId,
                Quantity= cartDetail.Quantity,
            });
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
    public CartDetailDto GetCartDetail(int accountId, int bookId)
    {
        var customerId = context.Customers.Where(customer => customer.AccountId == accountId).Select(customer => customer.Id).FirstOrDefault();
        var cart = context.Carts
            .Where(cart => cart.CustomerId == customerId)
            .Select(cart => new CartDto(cart.Id, cart.CustomerId)).FirstOrDefault();
        var cartDetail = context.CartDetail
            .Where(cartDetail => cartDetail.CartId == cart.Id && cartDetail.BookId==bookId).FirstOrDefault();
        return new CartDetailDto
        {
            BookId = cartDetail.BookId,
            CartId = cartDetail.CartId,
            Quantity = cartDetail.Quantity,
        };
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
    public void AddBookToCart(int bookId, int cartId, int quantity)
    {
        var cart = context.Carts.Find(cartId);
        var book = context.Books.Find(bookId);
        var cartDetail=context.CartDetail.Where(cartDetail=>cartDetail.CartId==cartId && cartDetail.BookId==bookId).FirstOrDefault();

        if (cartDetail!=null)
        {
            cartDetail.Quantity += quantity;
        }
        else
        {
            CartDetail newCartDetail = new CartDetail
            {
                Quantity = quantity,
            };
            context.CartDetail.Add(newCartDetail);
            newCartDetail.Cart= cart;
            newCartDetail.CartId= cartId;
            newCartDetail.Book= book;
            newCartDetail.BookId= bookId;
        }
        context.SaveChanges();
    }
    public void DeleteBookOfCart(int bookId, int accountId)
    {
        var cart=GetCartByAccountId(accountId);
        var cartDetail=context.CartDetail
            .Where(cartDetail=>cartDetail.BookId== bookId && cartDetail.CartId==cart.Id)
            .FirstOrDefault();
        if (cartDetail!=null)
        {
            context.CartDetail.Remove(cartDetail);
        }
        context.SaveChanges();
    }
    public void UpdateQuantityOfCartDetail(int accountId, int bookId, int quantity)
    {
        if (quantity <= 0) quantity = 1;
        var customerId = context.Customers.Where(customer => customer.AccountId == accountId).Select(customer => customer.Id).FirstOrDefault();
        var cart = context.Carts
            .Where(cart => cart.CustomerId == customerId)
            .Select(cart => new CartDto(cart.Id, cart.CustomerId)).FirstOrDefault();
        var cartDetail = context.CartDetail
            .Where(cartDetail => cartDetail.CartId == cart.Id && cartDetail.BookId == bookId).FirstOrDefault();
        if (cartDetail!=null)
        {
            cartDetail.Quantity = quantity;
            context.SaveChanges();
        }
    }
}
