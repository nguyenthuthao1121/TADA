<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> ThoaiVy

namespace TADA.Model.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
<<<<<<< HEAD
        public string Name { get; set; }
        public List<Book> Books { get; set; }
=======

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

>>>>>>> ThoaiVy
    }
}
