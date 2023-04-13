using Microsoft.EntityFrameworkCore;
using TADA.Dto;
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
        return new BookDto(context.Books.Find(cartDetail.BookId));
    }

}
