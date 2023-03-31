using TADA.Model.Entity;

namespace TADA.Dto;

public class BookDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Price { get; set; }
    public double Sale { get; set; }
    public string? Image { get; set; }
}
