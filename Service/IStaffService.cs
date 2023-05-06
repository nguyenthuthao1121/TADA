using TADA.Dto.Staff;

namespace TADA.Service;

public interface IStaffService
{
    StaffRoleDto GetStaffByAccountId(int id);
    List<StaffDto> GetAllStaffs();
    List<StaffDto> GetStaff(string search, string status, string sortBy, string sortType);
    void AddStaff(AddStaffDto staff);
}
