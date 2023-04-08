using TADA.Dto;

namespace TADA.Service;

public interface IStaffService
{
    StaffRoleDto GetStaffByAccountId(int id);
}
