using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto.Order;

public class RecentlyOrderDto
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public string Province { get; set; }
    public string Status { get; set; }

}
