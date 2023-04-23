using Microsoft.AspNetCore.Mvc;

namespace TADA.Dto.Staff;

public class AddStaffDto
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public DateTime Birthday { get; set; }
    public string TelephoneNumber { get; set; }
    public int RoleId { get; set; }
    public int WardId { get; set; }
    public string Street { get; set; }
    public int AddressId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public int AccountId { get; set; }
}
