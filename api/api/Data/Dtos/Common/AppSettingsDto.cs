namespace api.Data.Dtos.Common;

public class AppSettingsDto
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public SecretsDto Secrets { get; set; }

    public AppSettingsDto()
    {
        ConnectionStrings = new ConnectionStrings();
        Secrets = new SecretsDto();
    }

}
public class SecretsDto
{
    public string SymmetricSecurityKey { get; set; }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; }
}