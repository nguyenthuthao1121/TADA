namespace TADA.Dto.Address;

public class AddressDto
{
    public int AccountId { get; set; }
    public int AddressId { get; set; }
    public int WardId { get; set; }
    public int DistrictId { get; set; }
    public int ProvinceId { get; set; }
    public string Street { get; set; }
    public string WardName { get; set; }
    public string DistrictName { get; set; }
    public string ProvinceName { get; set; }

}
