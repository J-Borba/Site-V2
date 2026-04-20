namespace projects_api.Data.Dtos.User;

public record CreateUserDto(string Email, string Password, string? UserName);
