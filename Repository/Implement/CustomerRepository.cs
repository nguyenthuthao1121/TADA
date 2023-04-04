using TADA.Dto;
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
            /*            var customers = context.Customers
                            .Join(context.Accounts,
                            customer => customer.AccountId,
                            account => account.Id,
                            (customer, account) => new CustomerDto(
                                account.Id, account.Email, account.Password, account.CreateDate, account.Status, 
                                customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                )).ToList();
                        return customers;*/
            return null;
        }
        public List<CustomerDto> GetCustomers(string gender, string status, string sortBy, string sortType)
        {
            /*            switch (gender)
                        {
                            case "Male":
                                {
                                    switch (status)
                                    {
                                        case "Active":
                                            return context.Customers.Where(customer => customer.Gender == true)
                                                    .Join(context.Accounts.Where(account => account.Status == true),
                                                    customer => customer.AccountId,
                                                    account => account.Id,
                                                    (customer, account) => new CustomerDto(
                                                        account.Id, account.Email, account.Password, account.CreateDate, account.Status,
                                                        customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                                        )).ToList();
                                        case "Blocked":
                                            return context.Customers.Where(customer => customer.Gender == true)
                                                    .Join(context.Accounts.Where(account => account.Status == false),
                                                    customer => customer.AccountId,
                                                    account => account.Id,
                                                    (customer, account) => new CustomerDto(
                                                        account.Id, account.Email, account.Password, account.CreateDate, account.Status,
                                                        customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                                        )).ToList();
                                        default:
                                            return context.Customers.Where(customer => customer.Gender == true)
                                                    .Join(context.Accounts,
                                                    customer => customer.AccountId,
                                                    account => account.Id,
                                                    (customer, account) => new CustomerDto(
                                                        account.Id, account.Email, account.Password, account.CreateDate, account.Status,
                                                        customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                                        )).ToList();
                                    }
                                }
                            case "Female":
                                {
                                    switch (status)
                                    {
                                        case "Active":
                                            return context.Customers.Where(customer => customer.Gender == false)
                                                    .Join(context.Accounts.Where(account => account.Status == true),
                                                    customer => customer.AccountId,
                                                    account => account.Id,
                                                    (customer, account) => new CustomerDto(
                                                        account.Id, account.Email, account.Password, account.CreateDate, account.Status,
                                                        customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                                        )).ToList();
                                        case "Blocked":
                                            return context.Customers.Where(customer => customer.Gender == false)
                                                    .Join(context.Accounts.Where(account => account.Status == false),
                                                    customer => customer.AccountId,
                                                    account => account.Id,
                                                    (customer, account) => new CustomerDto(
                                                        account.Id, account.Email, account.Password, account.CreateDate, account.Status,
                                                        customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                                        )).ToList();
                                        default:
                                            return context.Customers.Where(customer => customer.Gender == false)
                                                    .Join(context.Accounts,
                                                    customer => customer.AccountId,
                                                    account => account.Id,
                                                    (customer, account) => new CustomerDto(
                                                        account.Id, account.Email, account.Password, account.CreateDate, account.Status,
                                                        customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                                        )).ToList();
                                    }
                                }
                            default:
                                {
                                    switch (status)
                                    {
                                        case "Active":
                                            return context.Customers
                                                    .Join(context.Accounts.Where(account => account.Status == true),
                                                    customer => customer.AccountId,
                                                    account => account.Id,
                                                    (customer, account) => new CustomerDto(
                                                        account.Id, account.Email, account.Password, account.CreateDate, account.Status,
                                                        customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                                        )).ToList();
                                        case "Blocked":
                                            return context.Customers
                                                    .Join(context.Accounts.Where(account => account.Status == false),
                                                    customer => customer.AccountId,
                                                    account => account.Id,
                                                    (customer, account) => new CustomerDto(
                                                        account.Id, account.Email, account.Password, account.CreateDate, account.Status,
                                                        customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                                        )).ToList();
                                        default:
                                            return context.Customers
                                                    .Join(context.Accounts,
                                                    customer => customer.AccountId,
                                                    account => account.Id,
                                                    (customer, account) => new CustomerDto(
                                                        account.Id, account.Email, account.Password, account.CreateDate, account.Status,
                                                        customer.Id, customer.Name, customer.Birthday, customer.Gender, customer.TelephoneNumber, customer.Address
                                                        )).ToList();
                                    }
                                }
                        }*/
            var customers = GetAllCustomers();
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
                case "Address": customers = customers.OrderBy(customer => customer.Address).ToList(); break;
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
        
    }
}
