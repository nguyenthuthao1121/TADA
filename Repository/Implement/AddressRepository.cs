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
    public string GetAddressByIdAndPart(int id, int part)
    {
        var address = context.Addresses.Find(id);
        if (part == 1) return address.Street;
        var ward = context.Wards.Find(address.WardId);
        if (part == 2) return ward.Name;
        var district=context.Districts.Find(ward.DistrictId);
        if (part == 3) return district.Name;
        var province=context.Provinces.Find(district.ProvinceId);
        return province.Name;
    }

    public void AddAddress(string street, int wardId)
    {
        context.Addresses.Add(new Address { Street= street, WardId = wardId });
        context.SaveChanges();
    }

    public int GetLastId()
    {
        return context.Addresses.ToList().Last().Id;
    }
}
