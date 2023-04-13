using TADA.Dto.Account;

namespace TADA.Service;

public interface IAuthenticationService
{
    AccountDto GetAccount(string email, string password);
}
