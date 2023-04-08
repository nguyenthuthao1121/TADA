using Microsoft.EntityFrameworkCore;
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

}
