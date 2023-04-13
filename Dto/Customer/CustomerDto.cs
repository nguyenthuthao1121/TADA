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
    public CustomerDto(int accountId, string email, string password, DateTime createDate, bool status, int customerId, string name, DateTime birthday, bool gender, string telephoneNumber, int addressId)
    {
        AccountId = accountId;
        Email = email;
        Password = password;
        CreateDate = createDate;
        Status = status;
        CustomerId = customerId;
        Name = name;
        Birthday = birthday;
        Gender = gender;
        TelephoneNumber = telephoneNumber;
        AddressId = addressId;
    }
    public CustomerDto(int accountId, string email, string password, DateTime createDate, bool status, int customerId, string name, DateTime birthday, bool gender, string telephoneNumber, int addressId, string address)
    {
        AccountId = accountId;
        Email = email;
        Password = password;
        CreateDate = createDate;
        Status = status;
        CustomerId = customerId;
        Name = name;
        Birthday = birthday;
        Gender = gender;
        TelephoneNumber = telephoneNumber;
        AddressId = addressId;
        Address = address;
    }
    public CustomerDto(Customer customer)
    {
        AccountId=customer.AccountId;
        CustomerId = customer.Id;
        Name= customer.Name;
        Birthday= customer.Birthday;
        Gender = customer.Gender;
        TelephoneNumber = customer.TelephoneNumber;
        AddressId = customer.AddressId;
    }
}
