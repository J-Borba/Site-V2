namespace projects_api.Common.Settings;

public class AppSettings
{
    public SecretsSettings Secrets { get; set; } = new();
}

public class SecretsSettings
{
    public string SymmetricSecurityKey { get; set; } = string.Empty;
}
