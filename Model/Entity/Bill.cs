using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DateTrade { get; set; }
        
        public int? ProviderId { get; set; }

        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }

        public List<BillDetail> BillDetails { get; } = new();

    }
}
