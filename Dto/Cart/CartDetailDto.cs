using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto.Cart;

public class CartDetailDto
{
    public int CartId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public CartDetailDto(int cartId, int bookId, int quantity)
    {
        CartId = cartId;
        BookId = bookId;
        Quantity = quantity;
    }

    public CartDetailDto(CartDetailDto cartDetailDto)
    {
        CartId = cartDetailDto.CartId;
        BookId = cartDetailDto.BookId;
        Quantity = cartDetailDto.Quantity;
    }
    public CartDetailDto(CartDetail cartDetail)
    {
        CartId = cartDetail.CartId;
        BookId = cartDetail.BookId;
        Quantity = cartDetail.Quantity;
    }

}
