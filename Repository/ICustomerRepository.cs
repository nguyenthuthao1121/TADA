﻿using TADA.Dto;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface ICustomerRepository
{
    List<CustomerDto> GetAllCustomers();
    List<CustomerDto> GetCustomers(string gender, string status, string sortBy, string sortType);
    
    string GetNameByAccountId(int id);
    int GetLastId();
}
