using System.Text.RegularExpressions;

namespace api.Common;

public class ValidateCPF
{
    public static bool IsValidCpf(string cpf)
    {
        // Remove qualquer pontuação do CPF
        cpf = cpf.Replace(".", "").Replace("-", "");

        // Verifica se o CPF tem 11 dígitos
        if (cpf.Length != 11)
        {
            return false;
        }

        bool allDigitsEqual = true;
        for (int i = 1; i < 11 && allDigitsEqual; i++)
        {
            if (cpf[i] != cpf[0])
            {
                allDigitsEqual = false;
            }
        }

        if (allDigitsEqual)
        {
            return false;
        }

        int[] numbers = new int[11];
        for (int i = 0; i < 11; i++)
            numbers[i] = int.Parse(cpf[i].ToString());

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (10 - i) * numbers[i];

        int result = sum % 11;
        if (result == 1 || result == 0)
        {
            if (numbers[9] != 0)
            {
                return false;
            }
        }
        else if (numbers[9] != 11 - result)
        {
            return false;
        }

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += (11 - i) * numbers[i];

        result = sum % 11;
        if (result == 1 || result == 0)
        {
            if (numbers[10] != 0)
            {
                return false;
            }
        }
        else if (numbers[10] != 11 - result)
        {
            return false;
        }

        return true;
    }

    public static bool IsCpfFormatValid(string cpf)
    {
        string pattern = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$";
        return Regex.IsMatch(cpf, pattern);
    }

}
