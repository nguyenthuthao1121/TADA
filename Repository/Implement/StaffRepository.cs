using TADA.Dto;
using TADA.Model;

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
                    RoleName = role.Name,
                }).FirstOrDefault();
    }
}
