using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto.Order;

public class OrderOfMonthDto
{
    public int OrderId { get; set; }
    public int StatusId { get; set; }
}
