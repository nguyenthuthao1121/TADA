using TADA.Dto.Customer;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface ICustomerRepository
{
    List<CustomerDto> GetAllCustomers();
    List<CustomerDto> GetCustomers(string gender, string status, string sortBy, string sortType);

    CustomerDto GetCustomerByAccountId(int accountId);
    CustomerDto GetCustomerById(int customerId);
    string GetNameByAccountId(int id);
    int GetIdByAccountId(int id);
    int GetLastId();
}
