namespace Invite.Commons;

public class References
{
    private static readonly Random random = new();
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-+/<>";

    public static string Generate()
    {
        char[] stringChars = new char[8];
        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}
