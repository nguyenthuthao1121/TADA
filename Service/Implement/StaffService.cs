using TADA.Dto.Staff;
using TADA.Repository;
using TADA.Repository.Implement;
using TADA.Utilities;

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
    public int GetNumOfStaffs()
    {
        return staffRepository.GetAllStaffs().Count;
    }
    public int GetNumOfStaffsByYear(int year)
    {
        return staffRepository.GetStaffsByYear((int)year).Count;
    }
    public List<StaffDto> GetStaff(string search, string status, string sortBy, string sortType)
    {
        List<StaffDto> list = new List<StaffDto>();
        foreach (StaffDto staff in staffRepository.GetStaff(status, sortBy, sortType))
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                list.Add(staff);
            }
            else
            {
                if ((UIHelper.RemoveUnicodeSymbol(staff.Name)).Contains(UIHelper.RemoveUnicodeSymbol(search)))
                {
                    list.Add(staff);
                }
            }
        }
        return list;
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
