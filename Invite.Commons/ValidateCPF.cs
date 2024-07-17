using System.Text.RegularExpressions;

namespace Invite.Commons;

public class ValidateCPF
{
    public static bool IsCpfFormatValid(string cpf)
    {
        string pattern = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$";
        return Regex.IsMatch(cpf, pattern);
    }
}
