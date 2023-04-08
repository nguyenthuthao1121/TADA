using TADA.Dto;
using TADA.Repository;

namespace TADA.Service.Implement;
public class StaffService : IStaffService
{
    private readonly IStaffRepository staffRepository;
    public StaffService(IStaffRepository staffRepository)
    {
        this.staffRepository = staffRepository;
    }
    public StaffRoleDto GetStaffByAccountId(int id)
    {
        return staffRepository.GetStaffByAccountId(id);
    }
}
