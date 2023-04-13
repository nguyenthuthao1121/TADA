using TADA.Dto.CustomerDto;

namespace TADA.Service;

public interface ICustomerService
{
    List<CustomerDto> GetAllCustomers();
    List<CustomerDto> GetCustomers(string gender, string status, string sortBy, string sortType);
    CustomerDto GetCustomerByAccountId(int accountId);
    string GetNameByAccountId(int id);
    int GetIdByAccountId(int id);
}
