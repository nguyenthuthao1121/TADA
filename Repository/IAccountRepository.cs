using TADA.Dto.Account;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IAccountRepository
{
    Account GetAccountById(int id);
    void AddNewAccount(AccountDto account);
    int GetLastId();
    void ChangeStatusOfAccount(int accountId);
    bool CheckExistEmail(string email);
    void ChangePassword(int accountId, string newPassword);
    int GetAccountIdByEmail(string email);
<<<<<<< HEAD
=======

>>>>>>> a78c7b6124fe6d1b37253b855aebee51f0ff7822
}
