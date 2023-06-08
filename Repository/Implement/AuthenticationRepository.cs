using Microsoft.EntityFrameworkCore;
using TADA.Dto.Account;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly TadaContext context;
    public AuthenticationRepository(TadaContext context)
    {
        this.context = context;
    }
    public Account GetAccount(string email, string password)
    {
        return context.Accounts.Where(account => account.Email.Equals(email)).Where(account => account.Password.Equals(password)).FirstOrDefault();
    }
    public AccountDto GetAccountByEmail(string email)
    {
        return context.Accounts.Where(account => account.Email.Equals(email)).Select(account => new AccountDto
        {
            Id = account.Id,
            Type = account.Type,
            Email = account.Email,
            Password = account.Password,
            CreateDate = account.CreateDate,
            Status = account.Status
        }).FirstOrDefault();
    }
}
