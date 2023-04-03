using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    [Column(TypeName = "char")]
    public string TelephoneNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateOrder { get; set; }

<<<<<<< HEAD
    public int AddressId { get; set; }
=======
        public int? AddressId { get; set; }
>>>>>>> 8704d3caa62f5f089e211017247bb897177b811d

    [ForeignKey("AddressId")]
    public Address Address { get; set; }

    public int CustomerId { get; set; }

<<<<<<< HEAD
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }
    public int StatusId { get; set; }

    [ForeignKey("StatusId")]
    public Status Status { get; set; }
=======
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int? StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public virtual List<Book>? Books { get; set; }
    }
>>>>>>> 8704d3caa62f5f089e211017247bb897177b811d
}
