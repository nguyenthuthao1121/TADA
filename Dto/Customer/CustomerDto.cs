using System.ComponentModel.DataAnnotations;
using TADA.Model.Entity;

namespace TADA.Dto.CustomerDto;

public class CustomerDto
{
    public int AccountId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; }
    public bool Status { get; set; }
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public bool Gender { get; set; }
    public string TelephoneNumber { get; set; }
    public int AddressId { get; set; }
    public string Address { get; set; }
    public CustomerDto()
    {

    }
    
}
