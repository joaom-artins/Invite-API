using System.Text.RegularExpressions;

namespace Invite.Commons;

public class CleanString
{
    public static string OnlyNumber(string input)
    {
        return Regex.Replace(input, "[^0-9]", "");
    }
}
