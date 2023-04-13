using Microsoft.EntityFrameworkCore.Metadata;
using TADA.Dto;

namespace TADA.Repository;

public interface IAddressRepository
{
    string GetStaffAddressByAccountId(int id);
    string GetCustomerAddressByAccountId(int id);
    string GetAddressById(int id);

}
