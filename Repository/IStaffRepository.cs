using TADA.Dto;

namespace TADA.Repository
{
    public interface IStaffRepository
    {
        StaffRoleDto GetStaffByAccountId(int id);
    }
}
