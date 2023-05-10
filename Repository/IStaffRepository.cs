using TADA.Dto.Customer;
using TADA.Dto.Staff;

namespace TADA.Repository
{
    public interface IStaffRepository
    {
        StaffRoleDto GetStaffByAccountId(int id);
        List<StaffDto> GetAllStaffs();
        List<StaffDto> GetStaff(string status, string sortBy, string sortType);
        StaffDto GetStaffDtoByAccountId(int accountId);
        void AddStaff(AddStaffDto staff);
        List<int> GetStaffsByYear(int year);
        void UpdateStaff(StaffDto staff);
    }
}
