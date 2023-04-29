using TADA.Dto.Order;
using TADA.Model.Entity;

namespace TADA.Utilities;

public class UIHelper
{
    public static string DisplayDateOrder(DateTime dateOrder)
    {
        if (dateOrder.Hour < 12)
        {
            return $"{dateOrder.Hour}:{dateOrder.Minute} AM {dateOrder.Day}/{dateOrder.Month}/{dateOrder.Year}";
        }
        else
        {
            return $"{dateOrder.Hour - 12}:{dateOrder.Minute} PM {dateOrder.Day}/{dateOrder.Month}/{dateOrder.Year}";
        }
    }
    public static string GetPriceString(int price)
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
