using TADA.Dto;
using TADA.Dto.Address;
using TADA.Model.Entity;
using TADA.Repository;

namespace TADA.Service.Implement;

public class AddressService : IAddressService
{
    private readonly IAddressRepository addressRepository;
    public AddressService(IAddressRepository addressRepository)
    {
        this.addressRepository = addressRepository;
    }


    public string GetAddressByIdAndType(int id, bool type)
    {
        if (type)
        {
            return addressRepository.GetCustomerAddressByAccountId(id);
        }
        else
        {
            return addressRepository.GetStaffAddressByAccountId(id);
        }
    }

    public string GetAddressById(int id)
    {
        return addressRepository.GetAddressById(id);
    }

    public string GetCustomerAddressByAccountId(int id)
    {
        return addressRepository.GetCustomerAddressByAccountId(id);
    }
    public string GetAddressByIdAndPart(int id, int part)
    {
        return addressRepository.GetAddressByIdAndPart(id, part);
    }
    public List<WardDto> GetAllWardsByDistrictId(int districtId)
    {
        return addressRepository.GetAllWardsByDistrictId(districtId);
    }

    public List<DistrictDto> GetAllDistrictsByProvinceId(int provinceId)
    {
        return addressRepository.GetAllDistrictsByProvinceId(provinceId);
    }

    public List<ProvinceDto> GetAllProvinces()
    {
        return addressRepository.GetAllProvinces();
    }
    public AddressDto GetCustomerAddressDto(int accountId)
    {
        return addressRepository.GetCustomerAddressDto(accountId);
    }

    public AddressDto GetOrderAddressDto(int orderId)
    {
        return addressRepository.GetOrderAddressDto(orderId);
    }
    public int AddNewAddress(string street, int wardId)
    {
        return addressRepository.AddNewAddress(street, wardId);
    }
}
