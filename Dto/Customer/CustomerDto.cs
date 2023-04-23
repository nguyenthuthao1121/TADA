using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TADA.Dto.Customer;

public class CustomerDto
{
    public int AccountId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; }
    public bool Status { get; set; }
    public int CustomerId { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Vui lòng lựa chọn ngày sinh!")]
    [DataType(DataType.Date)]
    //[Range(typeof(DateTime), "1900-01-01", "2024-12-12", ErrorMessage = "Vui lòng chọn ngày sinh trong khoảng từ {1} đến {2}!")]
    public DateTime Birthday { get; set; }
    public bool Gender { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ!")]
    public string TelephoneNumber { get; set; }
    public int AddressId { get; set; }
    public int WardId { get; set; }
    public string Address { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ!")]
    public string Street { get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string Province { get; set; }

    public CustomerDto()
    {

    }
    
}
