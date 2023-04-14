using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto.Cart;

public class CartDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public CartDto(int id, int customerId)
    {
        Id = id;
        CustomerId = customerId;
    }
    public CartDto(int customerId)
    {
        CustomerId = customerId;
    }

}
