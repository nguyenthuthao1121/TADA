using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TADA.Dto.Staff;

public class AddStaffDto
{
    [Required(ErrorMessage = "Vui lòng nhập tên nhân viên!")]
    public string Name { get; set; }
    public string Gender { get; set; }
    public DateTime Birthday { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Số điện thoại không hợp lệ")]
    public string TelephoneNumber { get; set; }
    public int RoleId { get; set; }
    [Required(ErrorMessage = "Vui lòng chọn xã/ phường !")]
    public int WardId { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ!")]
    public string Street { get; set; }
    public int AddressId { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập email!")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Email này không phải là email hợp lệ!")]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
    [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài tối thiểu là 6 ký tự!")]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu!")]
    [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng!")]
    public string ConfirmPassword { get; set; }
    public int AccountId { get; set; }
}
