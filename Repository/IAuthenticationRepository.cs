using TADA.Dto;
using TADA.Dto.Account;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IAuthenticationRepository
{
    Account GetAccount(string email, string password);
    AccountDto GetAccountByEmail(string email);
}
