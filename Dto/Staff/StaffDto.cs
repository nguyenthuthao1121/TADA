using System.ComponentModel.DataAnnotations;
using TADA.Model.Entity;

namespace TADA.Dto.Staff;

public class StaffDto
{
    public int AccountId { get; set; }
    [RegularExpression(@"^[a-zA-Z][-_.a-zA-Z0-9]{5,29}@g(oogle)?mail\.com$", ErrorMessage = "Vui lòng nhập email hợp lệ!")]
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; }
    public bool Status { get; set; }
    public int StaffId { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Vui lòng lựa chọn ngày sinh!")]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }
    public bool Gender { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
    [RegularExpression(@"^0(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$", ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ!")]
    public string TelephoneNumber { get; set; }
    public int AddressId { get; set; } 
    public string Address { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ!")]

    public string Street { get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string Province { get; set; }
    public int WardId { get; set; }

    public StaffDto()
    {

    }
    
}
