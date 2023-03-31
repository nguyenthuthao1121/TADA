using System.ComponentModel.DataAnnotations;

namespace TADA.Model.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
