using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto;

public class OrderDetailDto
{
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public OrderDetailDto()
    {
    }

    public OrderDetailDto (OrderDetail orderDetail)
    {
        OrderId= orderDetail.OrderId;
        BookId= orderDetail.BookId;
        Quantity = orderDetail.Quantity;
        Price = orderDetail.Price;
    }
    public string GetPriceString(int price)
    {
        string str = price.ToString();
        string tmp = "";
        while (str.Length > 3)
        {
            tmp = "." + str.Substring(str.Length - 3) + tmp;
            str = str.Substring(0, str.Length - 3);
        }
        tmp = str + tmp;
        return tmp;
    }

}
