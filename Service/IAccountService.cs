using TADA.Dto.Account;

namespace TADA.Service;

public interface IAccountService
{
    AccountDto GetAccountById(int id);
    void AddNewAccount(string email, string password, bool type);
    void ChangeStatusOfAccount(int accountId);
    bool CheckExistEmail(string email);
    int GetLastId();
}
