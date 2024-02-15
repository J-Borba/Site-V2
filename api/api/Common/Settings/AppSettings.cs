namespace api.Common.Settings;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public Secrets Secrets { get; set; }

    public AppSettings()
    {
        ConnectionStrings = new ConnectionStrings();
        Secrets = new Secrets();
    }
}
public class Secrets
{
    public string SymmetricSecurityKey { get; set; }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; }
}