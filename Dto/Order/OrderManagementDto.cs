namespace TADA.Dto.Order;

public class OrderManagementDto
{
    public int Id { get; set; }
    public DateTime DateOrder { get; set; }
    public string Address { get; set; }
    public string Province { get; set; }
    public string TelephoneNumber { get; set; }
    public int Price { get; set; }
    public string Status { get; set; }
}
