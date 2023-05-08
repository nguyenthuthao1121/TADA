using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
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
        public int Promotion { get; set; }
        public bool Hidden { get; set; }


        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public  Category Category { get; set; }
        public int ProviderId { get; set; }

        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }
        public List<OrderDetail> OrderDetails { get; } = new();
        public List<CartDetail> CartDetails { get; } = new();
        public List<Review> Reviews { get; set; }
    }
}
