using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using Microsoft.EntityFrameworkCore.Metadata;
using TADA.Dto;
using TADA.Dto.Address;
using TADA.Model.Entity;

namespace TADA.Repository;

public interface IAddressRepository
{
    string GetStaffAddressByAccountId(int id);
    string GetCustomerAddressByAccountId(int id);
    string GetAddressById(int id);
    string GetAddressByIdAndPart(int id, int part);
    AddressDto GetCustomerAddressDto(int accountId);
    List<WardDto> GetAllWardsByDistrictId(int districtId);
    List<DistrictDto> GetAllDistrictsByProvinceId(int provinceId);
    List<ProvinceDto> GetAllProvinces();
    void AddAddress(string street, int wardId);
    int GetLastId();
}
