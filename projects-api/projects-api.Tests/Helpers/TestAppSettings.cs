using projects_api.Common.Settings;

namespace projects_api.Tests.Helpers;

public static class TestAppSettings
{
    public static AppSettings Create() => new()
    {
        Secrets = new SecretsSettings
        {
            SymmetricSecurityKey = "test-super-secret-key-at-least-32-chars!!",
            JwtIssuer = "test-issuer",
            JwtAudience = "test-audience"
        }
    };
}
