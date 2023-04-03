using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity;

public class Review
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Column(TypeName = "nvarchar")]
    public string Comment { get; set; }
    public int Rating { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateReview { get; set; }

    [StringLength(255)]
    [Column(TypeName = "varchar")]
    public string Image { get; set; }
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }
    public int BookId { get; set; }

    [ForeignKey("BookId")]
    public Book Book { get; set; }


}
