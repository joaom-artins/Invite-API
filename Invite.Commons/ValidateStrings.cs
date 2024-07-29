namespace Invite.Commons;

public class ValidateStrings
{
    public static string Password => @"(?=[A-Za-z0-9 !#$%&()*+,-./:;<=>?@[\]^_{|}~]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[ !#$%&()*+,-./:;<=>?@[\]^_{|}~])(?=.{6,30}).*$";
    public static string PhoneNumber => @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$";
}