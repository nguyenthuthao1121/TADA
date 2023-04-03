using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace TADA.Model.Entity
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Author { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Genre { get; set; }
        public int Pages { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Weight { get; set; }

        [Column(TypeName = "money")]
        public int Price { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Cover { get; set; }
        public int Quantity { get; set; }

        [StringLength(255)]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar")]
        public string Image { get; set; }
        [Column(TypeName = "decimal")]
        public double Promotion { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual List<Order>? Orders { get; set; }
        public virtual List<Contract>? Contracts { get; set; }
        public virtual List<Cart>? Carts { get; set; }
        public virtual List<Review>? Reviews { get; set; }

    }
}
