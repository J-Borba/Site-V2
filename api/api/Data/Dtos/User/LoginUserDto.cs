using System.ComponentModel.DataAnnotations;

namespace api.Data.Dtos.User;

public class LoginUserDto
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
