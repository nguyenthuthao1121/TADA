using System.Net;
using TADA.Dto;
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
        var account = authenticationRepository.GetAccount(email, password);
        if (account == null) 
        {
            return null;
        }
        return new AccountDto(account.Id, account.Type, account.Email, account.Password, account.CreateDate, account.Status);
    }
}
