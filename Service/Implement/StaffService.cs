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
        try
        {
            return staffRepository.GetStaffByAccountId(id);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public List<StaffDto> GetAllStaffs()
    {
        try
        {
            return staffRepository.GetAllStaffs();
        }
        catch (Exception)
        {
            return new List<StaffDto>();
        }
        
    }
    public void AddStaff(AddStaffDto staff)
    {
        try
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
        catch (Exception)
        {
        }
        
    }
    public int GetNumOfStaffs()
    {
        try
        {
            return staffRepository.GetAllStaffs().Count;
        }
        catch (Exception)
        {
            return 0;
        }
        
    }
    public int GetNumOfStaffsByYear(int year)
    {
        try
        {
            return staffRepository.GetStaffsByYear((int)year).Count;
        }
        catch (Exception)
        {
            return 0;
        }
        
    }
    public List<StaffDto> GetStaff(string search, string status, string sortBy, string sortType)
    {
        try
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
        catch (Exception)
        {
            return new List<StaffDto>();
        }
        
    }

    public StaffDto GetStaffDtoByAccountId(int accountId)
    {
        try
        {
            return staffRepository.GetStaffDtoByAccountId((int)accountId);
        }
        catch (Exception)
        {
            return null;
        }
        
    }

    public void UpdateStaff(StaffDto staff)
    {
        try
        {
            staffRepository.UpdateStaff(staff);
        }
        catch (Exception)
        {
        }
        
    }
}
