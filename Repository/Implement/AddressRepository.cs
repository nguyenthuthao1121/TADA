using TADA.Model;
using TADA.Model.Entity;

namespace TADA.Repository.Implement;

public class AddressRepository : IAddressRepository
{
    private readonly TadaContext context;
    public AddressRepository(TadaContext context)
    {
        this.context = context;
    }
    public string GetCustomerAddressByAccountId(int id)
    {
        var addressId = context.Customers.Where(customer => customer.AccountId == id).Select(customer => customer.AddressId).FirstOrDefault();
        return GetAddressById(addressId);   
    }

    public string GetStaffAddressByAccountId(int id)
    {
        var addressId = context.Staff.Where(staff => staff.AccountId == id).Select(staff => staff.AddressId).FirstOrDefault();
        return GetAddressById((int)addressId);
    }
    public string GetAddressById(int id)
    {
        var address = context.Addresses.Where(address => address.Id == id).FirstOrDefault();
        if (address == null)
        {
            return null;
        }
        var ward = context.Wards.Where(ward => ward.Id == address.WardId).FirstOrDefault();
        var district = context.Districts.Where(district => district.Id == ward.DistrictId).FirstOrDefault();
        var province = context.Provinces.Where(province => province.Id == district.ProvinceId).FirstOrDefault();
        return $"{address.Street}, {ward.Name}, {district.Name}, {province.Name}";
    }
}
