using Microsoft.EntityFrameworkCore;
using TADA.Dto.Account;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement;

public class AccountRepository : IAccountRepository
{
    private readonly TadaContext context;
    public int LastId { get; set; }
    public AccountRepository(TadaContext context)
    {
        this.context = context;
    }

    public Account GetAccountById(int id)
    {
        return context.Accounts.Where(account => account.Id == id).FirstOrDefault();
    }
    public void AddNewAccount(AccountDto account)
    {
        Account newAccount = new Account
        {
            Type = account.Type,
            Email = account.Email,
            Password = account.Password,
            CreateDate = account.CreateDate,
            Status = account.Status,
        };
        context.Accounts.Add(newAccount);
        context.SaveChanges();

    }
    public int GetLastId() 
    { 
        return context.Accounts.Count();
    }

}
