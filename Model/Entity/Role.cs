using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity;

public class Role
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Column(TypeName = "nvarchar")]
    public string Name { get; set; }

}
