using TADA.Dto.Staff;

namespace TADA.Service;

public interface IStaffService
{
    StaffRoleDto GetStaffByAccountId(int id);
    List<StaffDto> GetAllStaffs();
    void AddStaff(AddStaffDto staff);
    int GetNumOfStaffs();
    int GetNumOfStaffsByYear(int year);
}
