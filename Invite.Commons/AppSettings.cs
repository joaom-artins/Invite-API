namespace Invite.Commons;

public class AppSettings
{
    public AppSettingsJwt Jwt { get; set; } = default!;
    public AppSettingsAsaas Asaas { get; set; } = default!;
}

public class AppSettingsJwt
{
    public string SecretKey { get; set; } = default!;
    public int Expiration { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
}

public class AppSettingsAsaas
{
    public string ApiKey { get; set; } = default!;
    public string ApiUrl { get; set; } = default!;
}