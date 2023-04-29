using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto.Cart;

public class CartDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<CartDetailDto> CartDetails { get; set; }
}
