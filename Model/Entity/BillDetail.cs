using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity
{
    public class BillDetail
    {
        public int BillId { get; set; }
        public int BookId { get; set; }
        public Bill Bill { get; set; } = null!;
        public Book Book { get; set; } = null!;
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public int CostPrice { get; set; }

    }
}
