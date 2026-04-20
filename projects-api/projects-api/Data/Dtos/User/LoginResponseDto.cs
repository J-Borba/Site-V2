using projects_api.Common.Validation;

namespace projects_api.Data.Dtos.User;

public class LoginResponseDto
{
    public ValidationResult Validation { get; } = new();
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = [];
}
