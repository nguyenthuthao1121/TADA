using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TADA.Model.Entity
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Street { get; set; }
        public int WardId { get; set; }

        [ForeignKey("WardId")]
        public Ward Ward { get; set; }
    }
}