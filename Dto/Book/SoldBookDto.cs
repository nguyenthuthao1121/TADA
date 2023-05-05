using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TADA.Model.Entity;

namespace TADA.Dto.Book;

public class SoldBookDto
{
    public int BookId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int SoldQuantity { get; set; }
}
