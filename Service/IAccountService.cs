using TADA.Dto.Account;

namespace TADA.Service;

public interface IAccountService
{
    AccountDto GetAccountById(int id);
    string GetPasswordByAccountId(int accountId);
    void AddNewAccount(string email, string password, bool type);
    void ChangeStatusOfAccount(int accountId);
    bool CheckExistEmail(string email);
    int GetLastId();
    void ChangePassword(int accountId, string newPassword);
    int GetAccountIdByEmail(string email);

}
