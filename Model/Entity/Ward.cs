using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TADA.Model.Entity
{
    public class Ward
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public District District { get; set; }
        public  List<Address>? Addresses { get; set; }
    }
}
