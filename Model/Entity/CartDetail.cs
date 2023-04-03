using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity
{
    public class CartDetail
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public Cart Cart { get; set; } = null!;
        public Book Book { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
