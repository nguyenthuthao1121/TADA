using Microsoft.Identity.Client;
using System.Net;
using TADA.Dto.Address;
using TADA.Dto.Customer;
using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TadaContext context;
        public CustomerRepository(TadaContext context)
        {
            this.context = context;
        }
        public List<CustomerDto> GetAllCustomers()
        {
            var customers = context.Customers
                .Join(context.Accounts,
                customer => customer.AccountId,
                account => account.Id,
                (customer, account) => new CustomerDto
                {
                    AccountId = account.Id,
                    Email = account.Email,
                    Password = account.Password,
                    CreateDate = account.CreateDate,
                    Status = account.Status,
                    CustomerId = customer.Id,
                    Name = customer.Name,
                    Birthday = customer.Birthday,
                    Gender = customer.Gender,
                    TelephoneNumber = customer.TelephoneNumber,
                    AddressId = customer.AddressId,
                }).ToList();
            customers.Reverse();
            return customers;
        }
        //public CustomerDto GetCustomerByAccountId(int id)
        //{
        //    var customer = context.Customers
        //        .Join(context.Accounts,
        //        customer => customer.AccountId,
        //        account => account.Id,
        //        (customer, account) => new CustomerDto(
        //            account.Id, account.Email, account.Password, account.CreateDate, account.Status,
        //            customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.AddressId
        //            )).ToList().Where(customeraccount => customeraccount.AccountId == id).FirstOrDefault();
        //    return customer;
        //}
        public CustomerDto GetCustomerById(int customerId)
        {
            var customer = context.Customers.Find(customerId);
            var account=context.Accounts.Find(customer.AccountId);
            return new CustomerDto
            {
                AccountId = account.Id,
                Email = account.Email,
                Password = account.Password,
                CreateDate = account.CreateDate,
                Status = account.Status,
                CustomerId = customer.Id,
                Name = customer.Name,
                Birthday = customer.Birthday,
                Gender = customer.Gender,
                TelephoneNumber = customer.TelephoneNumber,
                AddressId = customer.AddressId,
            };
        }
        public List<CustomerDto> GetCustomers(string search, string gender, string status, string sortBy, string sortType)
        {
            var customers = GetAllCustomers();
            if (!string.IsNullOrWhiteSpace(search))
            {
                customers = customers.Where(p => p.Name.Contains(search)).ToList();
            }
            switch (gender)
            {
                case "Male": customers = customers.Where(customer => customer.Gender == true).ToList(); break;
                case "Female": customers = customers.Where(customer => customer.Gender == false).ToList(); break;
                default: break;
            }
            switch (status)
            {
                case "Active": customers = customers.Where(customer => customer.Status == true).ToList(); break;
                case "Blocked": customers = customers.Where(customer => customer.Status == false).ToList(); break;
                default: break;
            }
            switch (sortBy)
            {
                case "Name": customers = customers.OrderBy(customer => customer.Name).ToList(); break;
                case "Email": customers = customers.OrderBy(customer => customer.Email).ToList(); break;
                default: break;
            }
            if (sortType == "Desc")
            {
                customers.Reverse();
            }
            return customers;
        }
        public string GetNameByAccountId(int id)
        {
            return context.Customers.Where(customer => customer.AccountId == id).Select(customer => customer.Name).FirstOrDefault();
        }
        public int GetLastId()
        {
            return context.Customers.Count();
        }
        public CustomerDto GetCustomerByAccountId(int accountId)
        {
            var account=context.Accounts.Find(accountId);
            if (account == null) return null;
            var customer = context.Customers.Where(customer => customer.AccountId == accountId)
                .Select(customer => customer).FirstOrDefault();
            AddressRepository addressRepository = new AddressRepository(context);
            AddressDto address = addressRepository.GetCustomerAddressDto(accountId);
            return new CustomerDto
            {
                AccountId= accountId,
                Email=account.Email,
                Password=account.Password,
                CreateDate=account.CreateDate,
                Status=account.Status,
                CustomerId= customer.Id,
                Name = customer.Name,
                Birthday=customer.Birthday,
                Gender=customer.Gender,
                TelephoneNumber=customer.TelephoneNumber,
                AddressId=customer.AddressId,
                Street = address.Street,
                Ward = address.WardName,
                District = address.DistrictName,
                Province = address.ProvinceName
            };
        }
        public int GetIdByAccountId(int id)
        {
            return context.Customers.Where(customer => customer.AccountId == id).Select(customer => customer.Id).FirstOrDefault();
        }

        public void UpdateCustomer(CustomerDto customer)
        {
            var updateCustomer = context.Customers.FirstOrDefault(p => p.AccountId == customer.AccountId);
            if (updateCustomer != null)
            {
                updateCustomer.Name = customer.Name;
                updateCustomer.TelephoneNumber = customer.TelephoneNumber;
                updateCustomer.Birthday= customer.Birthday;
                updateCustomer.Gender = customer.Gender;
                var entry = context.Entry(updateCustomer);
                entry.Reference(p => p.Address).Load();
                updateCustomer.Address.WardId = customer.WardId;
                updateCustomer.Address.Street = customer.Street;
                context.SaveChanges();
            }
        }
    }
}
