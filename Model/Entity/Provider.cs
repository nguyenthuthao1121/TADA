using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Mapping;

namespace TADA.Model.Entity
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }
        public int Status;

        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        public  List<Contract> Contracts { get; set; }

    }
}
