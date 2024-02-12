using System.ComponentModel.DataAnnotations;

namespace api.Data.Dtos.User;

public class LoginUserDto
{
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    public string Email { get; set; }
}
