﻿using TADA.Dto.Customer;

namespace TADA.Service;

public interface ICustomerService
{
    List<CustomerDto> GetAllCustomers();
    List<CustomerDto> GetCustomers(string gender, string status, string sortBy, string sortType);
    CustomerDto GetCustomerById(int customerId);
    CustomerDto GetCustomerByAccountId(int accountId);
    string GetNameByAccountId(int id);
    int GetIdByAccountId(int id);
    void UpdateCustomer(CustomerDto customer);
}
