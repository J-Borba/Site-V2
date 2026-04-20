namespace projects_api.Common.Settings;

public class AppSettings
{
    public SecretsSettings Secrets { get; set; } = new();
}

public class SecretsSettings
{
    public string SymmetricSecurityKey { get; set; } = string.Empty;
    public string SuperAdminEmail { get; set; } = string.Empty;
    public string SuperAdminPassword { get; set; } = string.Empty;
    public string JwtIssuer { get; set; } = string.Empty;
    public string JwtAudience { get; set; } = string.Empty;
}
