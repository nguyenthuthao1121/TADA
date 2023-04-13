using TADA.Dto.Account;
using TADA.Repository;

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
        return new AccountDto(account.Id, account.Type, account.Email, account.Password, account.CreateDate, account.Status);
    }

    public void AddNewAccount(string email, string password)
    {
        //AccountDto account = new AccountDto(accountRepository.GetLastId() + 1, true, email, password, DateTime.Now, true);
        AccountDto account = new AccountDto(true, email, password, DateTime.Now, true);
        accountRepository.AddNewAccount(account);
    }

}
