using System.ComponentModel.DataAnnotations;

namespace TADA.Model.Entity
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        [Required,StringLength(10)]
        public string TelephoneNumber { get; set; }
        [Required,StringLength(50)]
        public string Address { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
