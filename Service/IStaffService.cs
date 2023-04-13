using TADA.Dto.Staff;

namespace TADA.Service;

public interface IStaffService
{
    StaffRoleDto GetStaffByAccountId(int id);
}
