using TADA.Dto;
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

        public List<CustomerDto> GetAllCustomers()
        {
            return customerRepository.GetAllCustomers();
        }

        public CustomerDto GetCustomerByAccountId(int id)
        {
            return customerRepository.GetCustomerByAccountId(id);
        }

        public List<CustomerDto> GetCustomers(string gender, string status, string sortBy, string sortType)
        {
            return customerRepository.GetCustomers(gender, status, sortBy, sortType);
        }
        public string GetNameByAccountId(int id)
        {
            return customerRepository.GetNameByAccountId(id);
        }

    }
}
