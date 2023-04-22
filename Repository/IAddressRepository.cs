using Microsoft.EntityFrameworkCore.Metadata;
using TADA.Dto;

namespace TADA.Repository;

public interface IAddressRepository
{
    string GetStaffAddressByAccountId(int id);
    string GetCustomerAddressByAccountId(int id);
    string GetAddressById(int id);
    string GetAddressByIdAndPart(int id, int part);
    void AddAddress(string street, int wardId);
    int GetLastId();
}
