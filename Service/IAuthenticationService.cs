using TADA.Dto;

namespace TADA.Service;

public interface IAuthenticationService
{
    AccountDto GetAccount(string email, string password);
}
