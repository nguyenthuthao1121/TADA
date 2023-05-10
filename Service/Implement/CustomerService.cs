using System.Drawing.Printing;
using TADA.Dto.Customer;
using TADA.Model;
using TADA.Model.Entity;
using TADA.Repository;
using TADA.Repository.Implement;
using TADA.Utilities;

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
            List<CustomerDto> list = new List<CustomerDto>();
            foreach (CustomerDto customer in customerRepository.GetCustomers(gender, status, sortBy, sortType))
            {
                if (string.IsNullOrWhiteSpace(search))
                {
                    list.Add(customer);
                }
                else
                {
                    if ((UIHelper.RemoveUnicodeSymbol(customer.Name)).Contains(UIHelper.RemoveUnicodeSymbol(search)))
                    {
                        list.Add(customer);
                    }
                }
            }
            return list;
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
