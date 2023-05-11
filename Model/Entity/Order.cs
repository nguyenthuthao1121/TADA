using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity
{
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
        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; }
        [Column(TypeName = "money")]
        public int ShipFee { get; set; }

        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int? StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public List<OrderDetail> OrderDetails { get; } = new();
        public List<Review> Reviews { get; set; }
    }
}
