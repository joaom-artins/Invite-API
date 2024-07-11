namespace Invite.Commons;

public class AppSettings
{
    public AppSettingsJwt Jwt { get; set; } = default!;
}

public class AppSettingsJwt
{
    public string Key { get; set; } = default!;
    public int Expiration { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
}
