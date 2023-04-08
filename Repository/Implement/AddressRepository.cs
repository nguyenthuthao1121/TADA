﻿using TADA.Model;
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
        var addressId = context.Customers.Where(customer => customer.Id == id).Select(customer => customer.Id).FirstOrDefault();
        var address = context.Addresses.Where(address => address.Id == addressId).FirstOrDefault();
        if (address == null)
        {
            return null;
        }
        var ward = context.Wards.Where(ward => ward.Id == address.WardId).FirstOrDefault();
        var district = context.Districts.Where(district => district.Id == ward.DistrictId).FirstOrDefault();
        var province = context.Provinces.Where(province => province.Id == district.ProvinceId).FirstOrDefault();
        return $"{address.Street}, {ward.Name}, {district.Name}, {province.Name}";
    }

    public string GetStaffAddressByAccountId(int id)
    {
        var addressId = context.Staff.Where(staff => staff.Id == id).Select(staff => staff.Id).FirstOrDefault();
        var address = context.Addresses.Where(address => address.Id == addressId).FirstOrDefault();
        var ward = context.Wards.Where(ward => ward.Id == address.WardId).FirstOrDefault();
        var district = context.Districts.Where(district => district.Id == ward.DistrictId).FirstOrDefault();
        var province = context.Provinces.Where(province => province.Id == district.ProvinceId).FirstOrDefault();
        return $"{address.Street}, {ward.Name}, {district.Name}, {province.Name}";
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