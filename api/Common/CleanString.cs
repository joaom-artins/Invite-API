using System.Text.RegularExpressions;

namespace api.Common;

public static class CleanString
{
    public static string OnlyNumber(string input)
    {
        return Regex.Replace(input, "[^0-9]", "");
    }
}
