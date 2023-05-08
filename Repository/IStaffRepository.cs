using TADA.Dto.Customer;
using TADA.Dto.Staff;

namespace TADA.Repository
{
    public interface IStaffRepository
    {
        StaffRoleDto GetStaffByAccountId(int id);
        List<StaffDto> GetAllStaffs();
        List<StaffDto> GetStaff(string search, string status, string sortBy, string sortType);
        StaffDto GetStaffDtoByAccountId(int accountId);
        void AddStaff(AddStaffDto staff);
        void UpdateStaff(StaffDto staff);
    }
}
