using TADA.Dto.Staff;
using TADA.Repository;

namespace TADA.Service.Implement;
public class StaffService : IStaffService
{
    private readonly IAccountService accountService;
    private readonly IAddressService addressService;
    private readonly IStaffRepository staffRepository;
    public StaffService(IAccountService accountService, IAddressService addressService, IStaffRepository staffRepository)
    {
        this.accountService = accountService;
        this.addressService = addressService;
        this.staffRepository = staffRepository;
    }
    public StaffRoleDto GetStaffByAccountId(int id)
    {
        return staffRepository.GetStaffByAccountId(id);
    }
    public List<StaffDto> GetAllStaffs()
    {
        return staffRepository.GetAllStaffs();
    }
    public void AddStaff(AddStaffDto staff)
    {
        accountService.AddNewAccount(staff.Email, staff.Password, false);
        addressService.AddAddress(staff.Street, staff.WardId);
        staffRepository.AddStaff(new AddStaffDto
        {
            Name = staff.Name,
            Gender = staff.Gender,
            Birthday = staff.Birthday,
            TelephoneNumber = staff.TelephoneNumber,
            AccountId = accountService.GetLastId(),
            AddressId = addressService.GetLastId(),
            RoleId = staff.RoleId
        });
    }

    public List<StaffDto> GetStaff(string search, string status, string sortBy, string sortType)
    {
        return staffRepository.GetStaff(search, status, sortBy, sortType);
    }

    public StaffDto GetStaffDtoByAccountId(int accountId)
    {
        return staffRepository.GetStaffDtoByAccountId((int)accountId);
    }

    public void UpdateStaff(StaffDto staff)
    {
        staffRepository.UpdateStaff(staff);
    }
}
