using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TADA.Dto.Customer;

public class AddCustomerDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public bool Gender { get; set; }
    public string TelephoneNumber { get; set; }
    public int AddressId { get; set; }
    public int AccountId { get; set; }
}
