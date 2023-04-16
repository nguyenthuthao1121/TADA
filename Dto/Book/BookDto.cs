using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TADA.Model.Entity;

namespace TADA.Dto.Book;

public class BookDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int PublicationYear { get; set; }
    public string Genre { get; set; }
    public int Pages { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Weight { get; set; }
    public int Price { get; set; }
    public string Cover { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Promotion { get; set; }
    public string CategoryName { get; set; }
    public string ProviderName { get; set; }
    public int CategoryId { get; set; }
    public int ProviderId { get; set; }
    public double AverageRating { get; set; }
    public int NumberOfReview { get; set; }



    public int GetCurrentPrice()
    {
        return Price - Price * Promotion / 100;
    }
    public string GetPriceString(int price)
    {
        string str = price.ToString();
        string tmp = "";
        while(str.Length > 3)
        {
            tmp = "."+str.Substring(str.Length - 3)+tmp;
            str=str.Substring(0, str.Length - 3);
        }
        tmp = str+tmp;
        return tmp;
    }
}
