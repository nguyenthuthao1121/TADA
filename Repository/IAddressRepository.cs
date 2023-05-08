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
    string GetAddressByIdAndPart(int id, int part);//Part: 1 street, 2 ward, 3 district, 4 province
    AddressDto GetCustomerAddressDto(int accountId);
    AddressDto GetStaffAddressDto(int accountId);
    AddressDto GetOrderAddressDto(int orderId);
    int AddNewAddress(string street, int wardId);
    List<WardDto> GetAllWardsByDistrictId(int districtId);
    List<DistrictDto> GetAllDistrictsByProvinceId(int provinceId);
    List<ProvinceDto> GetAllProvinces();
    void AddAddress(string street, int wardId);
    int GetLastId();
    string GetProvinceById(int id);
}
