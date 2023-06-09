using Microsoft.Identity.Client;
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
        try
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
        catch(Exception)
        {
            return null;
        }
    }
    public string GetPasswordByAccountId(int accountId)
    {
        try
        {
            var account = accountRepository.GetAccountById(accountId);
            return account.Password;
        }
        catch (Exception)
        {
            return null;
        }
        
    }
    public void AddNewAccount(string email, string password, bool type)
    {
        try
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
        catch(Exception)
        {

        }
    }

    public void ChangeStatusOfAccount(int accountId)
    {
        try
        {
            accountRepository.ChangeStatusOfAccount(accountId);
        }
        catch (Exception) { }
    }

    public bool CheckExistEmail(string email)
    {
        try
        {
            return accountRepository.CheckExistEmail(email);
        }
        catch (Exception) { return false; }
    }
    public int GetLastId()
    {
        try
        {
            return accountRepository.GetLastId();

        }
        catch (Exception) { return 0; }
    }

    public void ChangePassword(int accountId, string newPassword)
    {
        try
        {
            string newPwd = HashPassword.Hash(newPassword);
            accountRepository.ChangePassword(accountId, newPwd);
        }
        catch (Exception) { }
    }
    public int GetAccountIdByEmail(string email)
    {
        try
        {
            return accountRepository.GetAccountIdByEmail(email);
        }
        catch (Exception) { return 0; }
    }
}
