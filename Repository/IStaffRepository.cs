﻿using TADA.Dto.Staff;

namespace TADA.Repository
{
    public interface IStaffRepository
    {
        StaffRoleDto GetStaffByAccountId(int id);
        List<StaffDto> GetAllStaffs();
        void AddStaff(AddStaffDto staff);
        List<int> GetStaffsByYear(int year);
    }
}
