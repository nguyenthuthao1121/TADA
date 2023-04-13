using TADA.Dto.Customer;

namespace TADA.Service;

public interface ICustomerService
{
    List<CustomerDto> GetAllCustomers();
    List<CustomerDto> GetCustomers(string gender, string status, string sortBy, string sortType);
    string GetNameByAccountId(int id);
}
