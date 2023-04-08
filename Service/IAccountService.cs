using TADA.Dto;

namespace TADA.Service;

public interface IAccountService
{
    AccountDto GetAccountById(int id);
    void AddNewAccount(string email, string password);
}
