using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TADA.Model.Entity
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public bool Type { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

    }
}
