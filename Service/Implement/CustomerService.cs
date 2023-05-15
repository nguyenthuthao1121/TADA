using System.ComponentModel.DataAnnotations;
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
        private readonly IAccountRepository accountRepository;
        private readonly IAddressRepository addressRepository;
        public CustomerService(ICustomerRepository customerRepository, IAccountRepository accountRepository, IAddressRepository addressRepository)
        {
            this.customerRepository = customerRepository;
            this.accountRepository = accountRepository;
            this.addressRepository = addressRepository;
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

        public int GetNumOfCustomers()
        {
            return customerRepository.GetAllCustomers().Count;
        }
        public int GetNumOfCustomersByYear(int year)
        {
            return customerRepository.GetCustomersByYear(year).Count;
        }
        public void AddDefaultCustomer(string email)
        {
            var accountId = accountRepository.GetAccountIdByEmail(email);
            var addressId = addressRepository.GetLastId();
            AddCustomerDto customer = new AddCustomerDto()
            {
                Name = "Khách hàng",
                Birthday = DateTime.Now,
                Gender = true,
                TelephoneNumber = "0123456789",
                AddressId = addressId,
                AccountId = accountId
            };
            customerRepository.AddCustomer(customer);
        }
    }
}
