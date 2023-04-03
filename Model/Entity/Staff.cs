using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity;

public class Staff
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    [Column(TypeName = "nvarchar")]
    public string Name { get; set; }

<<<<<<< HEAD
    [Column(TypeName = "date")]
    public DateOnly Birthday { get; set; }

    public bool Gender { get; set; }
    [Required, StringLength(50)]
    public string Hometown { get; set; }
=======
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Column(TypeName = "bit")]
        public bool Gender { get; set; }
>>>>>>> 8704d3caa62f5f089e211017247bb897177b811d

    [StringLength(10)]
    [Column(TypeName = "char")]

<<<<<<< HEAD
    public string TelephoneNumber { get; set; }
    public int AddressId { get; set; }
=======
        public string TelephoneNumber { get; set; }
        public int? AddressId { get; set; }
>>>>>>> 8704d3caa62f5f089e211017247bb897177b811d

    [ForeignKey("AddressId")]
    public Address Address { get; set; }
    public int AccountId { get; set; }

<<<<<<< HEAD
    [ForeignKey("AccountId")]
    public Account Account { get; set; }
    public int RoleId { get; set; }
=======
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public int? RoleId { get; set; }
>>>>>>> 8704d3caa62f5f089e211017247bb897177b811d

    [ForeignKey("RoleId")]
    public Role Role { get; set; }

}
