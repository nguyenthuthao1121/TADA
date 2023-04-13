using System.ComponentModel.DataAnnotations;

namespace TADA.Dto.Staff;

public class StaffRoleDto
{
    public int AccountId { get; set; }
    public string Name { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }

}
