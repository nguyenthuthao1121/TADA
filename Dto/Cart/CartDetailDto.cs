using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto.Cart;

public class CartDetailDto
{
    public int CartId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }

}
