namespace TADA.Dto.Address;

public class AddressDto
{
    public int AccountId { get; set; }
    public int AddressId { get; set; }
    //public string AddressName { get; set; }
    public string Street { get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string Province { get; set; }

}
