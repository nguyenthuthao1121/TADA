using TADA.Dto.Customer;
using TADA.Model;
using TADA.Repository;
using TADA.Repository.Implement;

namespace TADA.Service.Implement
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public CustomerDto GetCustomerById(int customerId)
        {
            return customerRepository.GetCustomerById(customerId);
        }
        public List<CustomerDto> GetAllCustomers()
        {
            return customerRepository.GetAllCustomers();
        }
        public CustomerDto GetCustomerByAccountId(int accountId)
        {
            return customerRepository.GetCustomerByAccountId(accountId);
        }

        public List<CustomerDto> GetCustomers(string search, string gender, string status, string sortBy, string sortType)
        {
            return customerRepository.GetCustomers(search, gender, status, sortBy, sortType);
        }

        public int GetIdByAccountId(int id)
        {
            return customerRepository.GetIdByAccountId(id);
        }

        public string GetNameByAccountId(int id)
        {
            return customerRepository.GetNameByAccountId(id);
        }

        public void UpdateCustomer(CustomerDto customer)
        {
            customerRepository.UpdateCustomer(customer);
        }
    }
}
