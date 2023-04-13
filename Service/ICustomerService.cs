using TADA.Dto;

namespace TADA.Service;

public interface ICustomerService
{
    List<CustomerDto> GetAllCustomers();
    CustomerDto GetCustomerByAccountId(int id);
    List<CustomerDto> GetCustomers(string gender, string status, string sortBy, string sortType);
    string GetNameByAccountId(int id);
}
