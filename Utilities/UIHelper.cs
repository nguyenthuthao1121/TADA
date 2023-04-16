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
}
