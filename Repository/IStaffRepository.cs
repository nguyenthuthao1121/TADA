using TADA.Dto.Staff;

namespace TADA.Repository
{
    public interface IStaffRepository
    {
        StaffRoleDto GetStaffByAccountId(int id);
        List<StaffDto> GetAllStaffs();
    }
}
