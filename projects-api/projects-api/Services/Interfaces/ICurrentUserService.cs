namespace projects_api.Services.Interfaces;

public interface ICurrentUserService
{
    string Id { get; }
    string Email { get; }
    string UserName { get; }
    IEnumerable<string> Roles { get; }
}
