using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using TADA.Dto;
using TADA.Dto.Address;
using TADA.Model.Entity;

namespace TADA.Service;

public interface IAddressService
{
    string GetAddressByIdAndType(int id, bool type);
    string GetAddressById(int id);
    string GetCustomerAddressByAccountId(int id);
    string GetAddressByIdAndPart(int id, int part);
    AddressDto GetCustomerAddressDto(int accountId);
    List<WardDto> GetAllWardsByDistrictId(int districtId);
    List<DistrictDto> GetAllDistrictsByProvinceId(int provinceId);
    List<ProvinceDto> GetAllProvinces();
}
