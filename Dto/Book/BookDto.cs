﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TADA.Model.Entity;
using Microsoft.Identity.Client;

namespace TADA.Dto.Book;

public class BookDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập tên sách!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập mã sách!")]

    public string ISBN { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên tác giả!")]
    public string Author { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập tên nhà xuất bản!")]

    public string Publisher { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập năm xuất bản!")]
    [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Vui lòng nhập năm hợp lệ!")]
    [Range(1900, 2023, ErrorMessage = "Vui lòng nhập năm hợp lệ")]
    public int PublicationYear { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập thể loại!")]
    public string Genre { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập số trang!")]
    [Range(1, 9999, ErrorMessage = "Vui lòng nhập nhỏ hơn 10000")]
    [RegularExpression(@"^(?=.*[1-9])\d+$", ErrorMessage = "Vui lòng nhập số trang hợp lệ!")]

    public int Pages { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập chiều dài!")]
    [Range(1, 99, ErrorMessage = "Vui lòng nhập nhỏ hơn 100")]
    [RegularExpression(@"^(?=.*[1-9])\d*(\.\d+)?$", ErrorMessage = "Vui lòng nhập chiều dài hợp lệ!")]
    public double Length { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập chiều rộng!")]
    [Range(1, 99, ErrorMessage = "Vui lòng nhập nhỏ hơn 100")]
    [RegularExpression(@"^(?=.*[1-9])\d*(\.\d+)?$", ErrorMessage = "Vui lòng nhập chiều rộng hợp lệ!")]

    public double Width { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập trọng lượng!")]
    [Range(10, 999, ErrorMessage = "Vui lòng nhập nhỏ hơn 1000")]
    [RegularExpression(@"^(?=.*[1-9])\d*(\.\d+)?$", ErrorMessage = "Vui lòng nhập trọng lượng hợp lệ!")]
    public double Weight { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập giá tiền!")]
    [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Vui lòng nhập giá tiền hợp lệ!")]
    public int Price { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập hình thức bìa!")]
    public string Cover { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập số lượng!")]
    [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Vui lòng nhập số lượng hợp lệ!")]
    [Range(1, 9999, ErrorMessage = "Vui lòng nhập nhỏ hơn 10000")]
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập giảm giá!")]
    [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Vui lòng nhập số phần trăm giảm giá hợp lệ!")]
    [Range(0, 99, ErrorMessage = "Vui lòng nhập nhỏ hơn 100")]
    public int Promotion { get; set; }
    public bool Hidden { get; set; }
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
