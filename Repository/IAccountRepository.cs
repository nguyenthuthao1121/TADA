using TADA.Dto;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IAccountRepository
{
    Account GetAccountById(int id);
    void AddNewAccount(AccountDto account);
    int GetLastId();
}
