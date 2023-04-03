using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TADA.Model.Entity;

public class District
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    [Column(TypeName = "nvarchar")]
    public string Name { get; set; }
    public int ProvinceId { get; set; }

<<<<<<< HEAD
    [ForeignKey("ProvinceId")]
    public Province Province { get; set; }
=======
        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
        public virtual List<Ward>? Wards { get; set; }
>>>>>>> 8704d3caa62f5f089e211017247bb897177b811d

}
