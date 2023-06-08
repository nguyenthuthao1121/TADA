using System.Net;
using TADA.Dto.Account;
using TADA.Model;
using TADA.Model.Entity;
using TADA.Repository;

namespace TADA.Service.Implement;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationRepository authenticationRepository;
    public AuthenticationService(IAuthenticationRepository authenticationRepository)
    {
        this.authenticationRepository = authenticationRepository;
    }

    public AccountDto GetAccount(string email, string password)
    {
        try
        {
            var account = authenticationRepository.GetAccount(email, password);
            if (account == null)
            {
                return null;
            }
            return new AccountDto()
            {
                Id = account.Id,
                Type = account.Type,
                Email = account.Email,
                Password = account.Password,
                CreateDate = account.CreateDate,
                Status = account.Status,
            };
        }
        catch (Exception)
        {
            return null;
        }
    }
    public AccountDto GetAccountByEmail(string email)
    {
        return authenticationRepository.GetAccountByEmail(email);
    }
}
