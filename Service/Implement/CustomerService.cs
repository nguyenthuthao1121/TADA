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
            try
            {
                return customerRepository.GetCustomerById(customerId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<CustomerDto> GetAllCustomers()
        {
            try
            {
                return customerRepository.GetAllCustomers();
            }
            catch (Exception)
            {
                return new List<CustomerDto>();
            }
           
        }
        public CustomerDto GetCustomerByAccountId(int accountId)
        {
            try
            {
                return customerRepository.GetCustomerByAccountId(accountId);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public List<CustomerDto> GetCustomers(string search, string gender, string status, string sortBy, string sortType)
        {
            try
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
            catch (Exception)
            {
                return new List<CustomerDto>();
            }
            
        }

        public int GetIdByAccountId(int id)
        {
            try
            {
                return customerRepository.GetIdByAccountId(id);
            }
            catch (Exception) { return 0; }
        }

        public string GetNameByAccountId(int id)
        {
            try
            {
                return customerRepository.GetNameByAccountId(id);
            }
            catch (Exception) { return null; }
            
        }

        public void UpdateCustomer(CustomerDto customer)
        {
            try
            {
                customerRepository.UpdateCustomer(customer);
            }
            catch (Exception) { }
            
        }

        public int GetNumOfCustomers()
        {
            try
            {
                return customerRepository.GetAllCustomers().Count;
            }
            catch (Exception) { return 0; }
            
        }
        public int GetNumOfCustomersByYear(int year)
        {
            try
            {
                return customerRepository.GetCustomersByYear(year).Count;
            }
            catch (Exception) { return 0; }
            
        }
        public void AddDefaultCustomer(string email)
        {
            try
            {
                var accountId = accountRepository.GetAccountIdByEmail(email);
                var addressId = addressRepository.GetLastId();
                AddCustomerDto customer = new AddCustomerDto()
                {
                    Name = "Khách hàng",
                    Birthday = DateTime.Now,
                    Gender = true,
                    TelephoneNumber = "0222222222",
                    AddressId = addressId,
                    AccountId = accountId
                };
                customerRepository.AddCustomer(customer);
            }
            catch(Exception) { }
        }
    }
}
