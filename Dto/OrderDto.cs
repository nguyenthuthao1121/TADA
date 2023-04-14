using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public string TelephoneNumber { get; set; }
    public DateTime DateOrder { get; set; }
    public int? AddressId { get; set; }
    public int CustomerId { get; set; }
    public int StatusId { get; set; }
}
