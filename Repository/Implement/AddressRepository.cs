using Microsoft.Identity.Client;
using TADA.Dto.Address;
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
    public AddressDto GetCustomerAddressDto(int accountId)
    {
        var addressId = context.Customers.Where(cus => cus.AccountId == accountId).Select(cus => cus.AddressId).FirstOrDefault();
        var address = context.Addresses.Where(address => address.Id == addressId).FirstOrDefault();
        if (address == null)
        {
            return null;
        }
        var ward = context.Wards.Where(ward => ward.Id == address.WardId).FirstOrDefault();
        var district = context.Districts.Where(district => district.Id == ward.DistrictId).FirstOrDefault();
        var province = context.Provinces.Where(province => province.Id == district.ProvinceId).FirstOrDefault();
        return new AddressDto
        {
            Street = address.Street,
            WardId = ward.Id,
            WardName = ward.Name,
            DistrictId = district.Id,
            DistrictName = district.Name,
            ProvinceId = province.Id,
            ProvinceName = province.Name
        };
    }
    public List<WardDto> GetAllWardsByDistrictId(int districtId)
    {
        return context.Wards.OrderBy(p => p.Name).Where(p => p.DistrictId == districtId).Select(p => new WardDto
        {
            WardId = p.Id,
            WardName = p.Name
        }).ToList();
    }

    public List<DistrictDto> GetAllDistrictsByProvinceId(int provinceId)
    {
        return context.Districts.OrderBy(p => p.Name).Where(p => p.ProvinceId == provinceId).Select(p => new DistrictDto
        {
            DistrictId = p.Id,
            DistrictName = p.Name
        }).ToList();
    }

    public List<ProvinceDto> GetAllProvinces()
    {
        return context.Provinces.OrderBy(p=> p.Name).Select(p => new ProvinceDto
        {
            ProvinceId = p.Id,
            ProvinceName = p.Name
        }).ToList();
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
    public int AddNewAddress(string street, int wardId)
    {
        var ward = context.Wards.Find(wardId);
        Address address = new Address
        {
            Street = street,
        };
        context.Addresses.Add(address);
        if (ward != null)
        {
            address.Ward = ward;
            address.WardId = wardId;
        }
        context.SaveChanges();
        return context.Addresses.Where(address=>address.Street== street && address.WardId==wardId)
            .Select(address=>address.Id).FirstOrDefault();
    }
    public AddressDto GetOrderAddressDto(int orderId)
    {
        var addressId = context.Orders.Where(or => or.Id == orderId).Select(or => or.AddressId).FirstOrDefault();
        var address = context.Addresses.Where(address => address.Id == addressId).FirstOrDefault();
        if (address == null)
        {
            return null;
        }
        var ward = context.Wards.Where(ward => ward.Id == address.WardId).FirstOrDefault();
        var district = context.Districts.Where(district => district.Id == ward.DistrictId).FirstOrDefault();
        var province = context.Provinces.Where(province => province.Id == district.ProvinceId).FirstOrDefault();
        return new AddressDto
        {
            Street = address.Street,
            WardId = ward.Id,
            WardName = ward.Name,
            DistrictId = district.Id,
            DistrictName = district.Name,
            ProvinceId = province.Id,
            ProvinceName = province.Name
        };
    }
}
