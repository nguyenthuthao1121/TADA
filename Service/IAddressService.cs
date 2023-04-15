using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using TADA.Dto;

namespace TADA.Service;

public interface IAddressService
{
    string GetAddressByIdAndType(int id, bool type);
    string GetAddressById(int id);
    string GetCustomerAddressByAccountId(int id);
    string GetAddressByIdAndPart(int id, int part);
}
