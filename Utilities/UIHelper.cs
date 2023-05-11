using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

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
    public string NormalizeQueryString(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return string.Empty;
        }

        // Convert to lowercase and remove diacritics
        var normalizedQuery = query.ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        // Remove any characters that are not letters or digits
        foreach (var c in normalizedQuery)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark &&
                (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)))
            {
                sb.Append(c);
            }
        }

        // Return the normalized query string
        return sb.ToString().Normalize(NormalizationForm.FormC);
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
    public static string RemoveUnicodeSymbol(string word)
    {
        Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
        string temp = word.Normalize(NormalizationForm.FormD);
        return regex.Replace(temp, String.Empty).Replace('đ', 'd').Replace('Đ', 'D').ToLower();
    }
    
}

