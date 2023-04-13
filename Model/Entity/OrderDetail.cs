using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public Order Order { get; set; } = null!;
        public Book Book { get; set; } = null!;
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public int Price { get; set; }

    }
}
