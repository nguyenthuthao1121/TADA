using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto.Order;

public class OrderDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ!")]
    public string TelephoneNumber { get; set; }
    public DateTime DateOrder { get; set; }
    public DateTime UpdateDate { get; set; }
    public int ShipFee { get; set; }
    public int? AddressId { get; set; }
    public int WardId { get; set; }
    public int CustomerId { get; set; }
    public int StatusId { get; set; }
    public string Address { get; set; }
    [Required]
    public string Street { get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string Province { get; set; }
}
