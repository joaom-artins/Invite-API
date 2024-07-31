namespace Invite.Commons;

public class ValidateStrings
{
    public static string Password => @"(?=[A-Za-z0-9 !#$%&()*+,-./:;<=>?@[\]^_{|}~]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[ !#$%&()*+,-./:;<=>?@[\]^_{|}~])(?=.{6,30}).*$";
    public static string PhoneNumber => @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$";
    public static string CNPJ = @"^\d{14}$|^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$";
}