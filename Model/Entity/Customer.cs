using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity;

public class Customer
{
<<<<<<< HEAD
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
=======
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Column(TypeName = "bit")]
        public bool Gender { get; set; }

        [StringLength(10)]
        [Column(TypeName = "char")]
        public string TelephoneNumber { get; set; }
        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        public virtual List<Order>? Orders { get; set; }
        public virtual List<Review>? Reviews { get; set; }

    }
}
>>>>>>> 8704d3caa62f5f089e211017247bb897177b811d
