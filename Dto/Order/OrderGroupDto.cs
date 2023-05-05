using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto.Order;

public class OrderGroupDto
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
}
