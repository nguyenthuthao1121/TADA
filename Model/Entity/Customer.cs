using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateOnly Birthday { get; set; }

        public bool Gender { get; set; }

        [StringLength(10)]
        [Column(TypeName = "char")]
        public string TelephoneNumber { get; set; }
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }


    }
}
