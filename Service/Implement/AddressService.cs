using TADA.Dto;
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


}
