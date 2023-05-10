using TADA.Dto.Staff;

namespace TADA.Service;

public interface IStaffService
{
    StaffRoleDto GetStaffByAccountId(int id);
    StaffDto GetStaffDtoByAccountId(int accountId);
    void UpdateStaff(StaffDto staff);

    List<StaffDto> GetAllStaffs();
    List<StaffDto> GetStaff(string search, string status, string sortBy, string sortType);
    void AddStaff(AddStaffDto staff);
    int GetNumOfStaffs();
    int GetNumOfStaffsByYear(int year);
}
