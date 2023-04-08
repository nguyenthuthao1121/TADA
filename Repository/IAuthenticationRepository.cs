using TADA.Dto;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IAuthenticationRepository
{
    Account GetAccount(string email, string password);
}
