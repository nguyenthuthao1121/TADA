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

    public void ChangeStatusOfAccount(int accountId)
    {
        var account = context.Accounts.Find(accountId);
        account.Status = !account.Status;
        context.SaveChanges();
    }
    public bool CheckExistEmail(string email)
    {
        var account = context.Accounts.Where(account => account.Email == email).FirstOrDefault();
        if (account != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
