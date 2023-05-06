using TADA.Dto.Staff;

namespace TADA.Repository
{
    public interface IStaffRepository
    {
        StaffRoleDto GetStaffByAccountId(int id);
        List<StaffDto> GetAllStaffs();
        List<StaffDto> GetStaff(string search, string status, string sortBy, string sortType);
        void AddStaff(AddStaffDto staff);
    }
}
