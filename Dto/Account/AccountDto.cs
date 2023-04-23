namespace TADA.Dto.Account;

public class AccountDto
{
    public int Id { get; set; }
    public bool Type { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; }
    public bool Status { get; set; }

}
