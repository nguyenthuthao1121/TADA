using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TADA.Dto.Staff;
using TADA.Model;
using TADA.Model.Entity;
using static System.Reflection.Metadata.BlobBuilder;
using TADA.Dto.Address;
using TADA.Dto.Customer;

namespace TADA.Repository.Implement;

public class StaffRepository : IStaffRepository
{
    private readonly TadaContext context;
    public StaffRepository(TadaContext context)
    {
        this.context = context;
    }

    public StaffRoleDto GetStaffByAccountId(int id)
    {
        return context.Staff.Where(staff => staff.AccountId == id)
                .Join(context.Roles, staff => staff.RoleId, role => role.Id, (staff, role) => new StaffRoleDto
                {
                    AccountId = staff.AccountId,
                    Name = staff.Name,
                    RoleId = role.Id,
                    RoleName = role.Name
                }).FirstOrDefault();
    }
    public List<StaffDto> GetAllStaffs()
    {
        return context.Accounts
            .Where(account => account.Type == false)
            .Join(context.Staff, account => account.Id, staff => staff.AccountId,
            (account, staff) => new StaffDto
            {
                AccountId = account.Id,
                Email = account.Email,
                Password = account.Password,
                CreateDate = account.CreateDate,
                Status = account.Status,
                StaffId = staff.Id,
                Name = staff.Name,
                Birthday = staff.Birthday,
                Gender = staff.Gender,
                TelephoneNumber = staff.TelephoneNumber,
                AddressId = (int)staff.AddressId,
                RoleId = (int)staff.RoleId
            }).ToList();
    }
    public void AddStaff(AddStaffDto staff)
    {
        context.Staff.Add(new Staff
        {
            Name = staff.Name,
            Birthday = staff.Birthday,
            Gender = staff.Gender == "Nam" ? true : false,
            TelephoneNumber = staff.TelephoneNumber,
            AddressId = staff.AddressId,
            AccountId = staff.AccountId,
            RoleId = staff.RoleId
        });
        context.SaveChanges();
    }
    public List<int> GetStaffsByYear(int year)
    {
        return context.Accounts.Where(account => account.CreateDate.Year == year).Join(context.Staff, account => account.Id, staff => staff.AccountId, (account, staff) => staff.Id).ToList();
    }

    public List<StaffDto> GetStaff(string status, string sortBy, string sortType)
    {
        var staff = GetAllStaffs();
        switch (status)
            {
                case "true":
                    staff = staff.Where(p => p.Status == true).ToList(); break;
                case "false":
                    staff = staff.Where(p => p.Status == false).ToList(); break;
                default:
                    break;
            }
        switch (sortType)
            {
                case "desc":
                    switch (sortBy)
                    {
                        case "name":
                            staff = staff.OrderByDescending(p => p.Name).ToList();
                            break;
                        case "status":
                            staff = staff.OrderByDescending(p => p.Status).ToList();
                            break;
                        default:
                            staff = staff.OrderByDescending(p => p.StaffId).ToList();
                            break;
                    }
                    break;
                default:
                    switch (sortBy)
                    {
                        case "name":
                            staff = staff.OrderBy(p => p.Name).ToList();
                            break;
                        case "status":
                            staff = staff.OrderBy(p => p.Status).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
            }
        return staff;
    }
    public StaffDto GetStaffDtoByAccountId(int accountId)
    {
        var account = context.Accounts.Find(accountId);
        if (account == null) return null;
        var staff = context.Staff.Where(staff => staff.AccountId == accountId)
            .Select(s => s).FirstOrDefault();
        AddressRepository addressRepository = new AddressRepository(context);
        AddressDto address = addressRepository.GetStaffAddressDto(accountId);
        return new StaffDto
        {
            AccountId = accountId,
            Email = account.Email,
            Password = account.Password,
            CreateDate = account.CreateDate,
            Status = account.Status,
            StaffId = staff.Id,
            Name = staff.Name,
            Birthday = staff.Birthday,
            Gender = staff.Gender,
            TelephoneNumber = staff.TelephoneNumber,
            AddressId = (int)staff.AddressId,
            Street = address.Street,
            Ward = address.WardName,
            District = address.DistrictName,
            Province = address.ProvinceName
        };
    }

    public void UpdateStaff(StaffDto staff)
    {
        var updateStaff = context.Staff.FirstOrDefault(p => p.AccountId == staff.AccountId);
        if (updateStaff != null)
        {
            updateStaff.Name = staff.Name;
            updateStaff.TelephoneNumber = staff.TelephoneNumber;
            updateStaff.Birthday = staff.Birthday;
            updateStaff.Gender = staff.Gender;
            var entry = context.Entry(updateStaff);
            entry.Reference(p => p.Address).Load();
            updateStaff.Address.WardId = staff.WardId;
            updateStaff.Address.Street = staff.Street;
            context.SaveChanges();
        }
    }
}
