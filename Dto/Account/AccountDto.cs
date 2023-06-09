using System.ComponentModel.DataAnnotations;

namespace TADA.Dto.Account;

public class AccountDto
{
    public int Id { get; set; }
    public bool Type { get; set; }

    [RegularExpression(@"^[a-zA-Z][-_.a-zA-Z0-9]{5,29}@g(oogle)?mail\.com$", ErrorMessage = "Vui lòng nhập email hợp lệ!")]
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; }
    public bool Status { get; set; }

}
