using System.ComponentModel.DataAnnotations;

namespace TADA.Model.Entity;

public class Role
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
