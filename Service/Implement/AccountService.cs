using TADA.Dto.Account;
using TADA.Repository;
using TADA.Utilities;

namespace TADA.Service.Implement;

public class AccountService : IAccountService
{
    private readonly IAccountRepository accountRepository;
    public AccountService(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public AccountDto GetAccountById(int id)
    {
        var account = accountRepository.GetAccountById(id);
        return new AccountDto()
        {
            Id = account.Id,
            Type = account.Type,
            Email = account.Email,
            Password = account.Password,
            CreateDate = account.CreateDate,
            Status = account.Status
        };
    }
    public string GetPasswordByAccountId(int accountId)
    {
        var account = accountRepository.GetAccountById(accountId);
        return account.Password;
    }
    public void AddNewAccount(string email, string password, bool type)
    {
        //AccountDto account = new AccountDto(accountRepository.GetLastId() + 1, true, email, password, DateTime.Now, true);
        AccountDto account = new AccountDto()
        {
            Email = email,
            Password = password,
            Type = type,
            CreateDate = DateTime.Now,
            Status = true
        };
        accountRepository.AddNewAccount(account);
    }

    public void ChangeStatusOfAccount(int accountId)
    {
        accountRepository.ChangeStatusOfAccount(accountId);
    }

    public bool CheckExistEmail(string email)
    {
        return accountRepository.CheckExistEmail(email);
    }
    public int GetLastId()
    {
        return accountRepository.GetLastId();
    }

    public void ChangePassword(int accountId, string newPassword)
    {
        string newPwd = HashPassword.Hash(newPassword);
        accountRepository.ChangePassword(accountId, newPwd);
    }
    public int GetAccountIdByEmail(string email)
    {
        return accountRepository.GetAccountIdByEmail(email);
    }
}
