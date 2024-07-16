namespace Invite.Commons;

public  class ValidateStrings
{
    public static string Password => @"(?=[A-Za-z0-9 !#$%&()*+,-./:;<=>?@[\]^_{|}~]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[ !#$%&()*+,-./:;<=>?@[\]^_{|}~])(?=.{6,30}).*$";
}
