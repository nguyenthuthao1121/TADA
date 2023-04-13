using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TADA.Model.Entity;

namespace TADA.Dto.BookDto;

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
    public BookDto()
    {

    }

    public BookDto(int id, string name, string author, string publisher, int publicationYear, string genre, int pages, double length, double width, double weight, int price, string cover, int quantity, string description, string image, int promotion, int categoryId)
    {
        Id = id;
        Name = name;
        Author = author;
        Publisher = publisher;
        PublicationYear = publicationYear;
        Genre = genre;
        Pages = pages;
        Length = length;
        Width = width;
        Weight = weight;
        Price = price;
        Cover = cover;
        Quantity = quantity;
        Description = description;
        Image = image;
        Promotion = promotion;
        CategoryId = categoryId;
    }
    public BookDto(Book book)
    {
        Id = book.Id;
        Name = book.Name;
        Author = book.Author;
        Publisher = book.Publisher;
        PublicationYear = book.PublicationYear;
        Genre = book.Genre;
        Pages = book.Pages;
        Length = book.Length;
        Width = book.Width;
        Weight = book.Weight;
        Price = book.Price;
        Cover = book.Cover;
        Quantity = book.Quantity;
        Description = book.Description;
        Image = book.Image;
        Promotion = book.Promotion;
        CategoryId = book.CategoryId;
    }
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
