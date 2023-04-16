namespace TADA.Dto.Book;

public class BookManagementDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Price { get; set; }
    public int Promotion { get; set; }
    public int Quantity { get; set; }
    public string Image { get; set; }
    public string Category { get; set; }
    public string Provider { get; set; }
    public BookManagementDto()
    {

    }
    public int GetCurrentPrice()
    {
        return Price - Price * Promotion / 100;
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
