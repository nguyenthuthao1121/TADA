using System.ComponentModel.DataAnnotations;

namespace TADA.Model.Entity
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public bool Type { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

    }
}
