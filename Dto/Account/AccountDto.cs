namespace TADA.Dto.Account;

public class AccountDto
{
    public int Id { get; set; }
    public bool Type { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; }
    public bool Status { get; set; }

    public AccountDto(int id, bool type, string email, string password, DateTime createDate, bool status)
    {
        Id = id;
        Type = type;
        Email = email;
        Password = password;
        CreateDate = createDate;
        Status = status;
    }
    public AccountDto(bool type, string email, string password, DateTime createDate, bool status)
    {
        Type = type;
        Email = email;
        Password = password;
        CreateDate = createDate;
        Status = status;
    }
}
