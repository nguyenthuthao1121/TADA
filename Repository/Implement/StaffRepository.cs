using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TADA.Dto.Staff;
using TADA.Model;
using TADA.Model.Entity;
using static System.Reflection.Metadata.BlobBuilder;

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

    public List<StaffDto> GetStaff(string search, string status, string sortBy, string sortType)
    {
        var staff = GetAllStaffs();
        if (string.IsNullOrEmpty(search))
        {
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
        }
        else
        {
            switch (status)
            {
                case "true":
                    staff = staff.Where(p => p.Status == true && p.Name.Contains(search)).ToList(); break;
                case "false":
                    staff = staff.Where(p => p.Status == false && p.Name.Contains(search)).ToList(); break;
                default:
                    staff = staff.Where(p => p.Name.Contains(search)).ToList(); break;
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
        }
        return staff;
    }
}
