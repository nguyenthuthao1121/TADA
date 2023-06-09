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
        try
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
        catch (Exception)
        {
            return null;
        }
    }

    public string GetAddressById(int id)
    {
        try
        {
            return addressRepository.GetAddressById(id);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public string GetCustomerAddressByAccountId(int id)
    {
        try
        {
            return addressRepository.GetCustomerAddressByAccountId(id);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public string GetAddressByIdAndPart(int id, int part)
    {
        try
        {
            return addressRepository.GetAddressByIdAndPart(id, part);
        }
        catch (Exception)
        {
            return null;
        }
        
    }
    public List<WardDto> GetAllWardsByDistrictId(int districtId)
    {
        try
        {
            return addressRepository.GetAllWardsByDistrictId(districtId);
        }
        catch (Exception)
        {
            return new List<WardDto>();
        }
        
    }

    public void AddAddress(string street, int wardId)
    {
        try
        {
            addressRepository.AddAddress(street, wardId);
        }
        catch (Exception)
        {
        }
        
    }
    public int GetLastId()
    {
        try
        {
            return addressRepository.GetLastId();
        }
        catch (Exception)
        {
            return 0;
        }
        
    }

    public List<DistrictDto> GetAllDistrictsByProvinceId(int provinceId)
    {
        try
        {
            return addressRepository.GetAllDistrictsByProvinceId(provinceId);
        }
        catch (Exception)
        {
            return new List<DistrictDto>();
        }
        
    }

    public List<ProvinceDto> GetAllProvinces()
    {
        try
        {
            return addressRepository.GetAllProvinces();
        }
        catch (Exception)
        {
            return new List<ProvinceDto>();
        }
        
    }
    public AddressDto GetCustomerAddressDto(int accountId)
    {
        try
        {
            return addressRepository.GetCustomerAddressDto(accountId);
        }
        catch (Exception)
        {
            return null;
        }
        
    }

    public AddressDto GetOrderAddressDto(int orderId)
    {
        try
        {
            return addressRepository.GetOrderAddressDto(orderId);
        }
        catch (Exception)
        {
            return null;
        }
        
    }
    public int AddNewAddress(string street, int wardId)
    {
        try
        {
            return addressRepository.AddNewAddress(street, wardId);
        }
        catch (Exception)
        {
            return 0;
        }
        
    }

    public AddressDto GetStaffAddressDto(int accountId)
    {
        try
        {
            return addressRepository.GetStaffAddressDto(accountId);
        }
        catch (Exception)
        {
            return null;
        }
        
    }
    
    public void AddDefaultAddress()
    {
        try
        {
            addressRepository.AddAddress("Không xác định", 1);
        }
        catch (Exception)
        {
        }
        
    }
}
