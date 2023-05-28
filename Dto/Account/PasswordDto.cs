using System.ComponentModel.DataAnnotations;

namespace TADA.Dto.Account
{
    public class PasswordDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài ít nhất là 6 ký tự!")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập vào trường này!")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không đúng!")]
        public string ConfirmPassword { get; set; }

    }
}
