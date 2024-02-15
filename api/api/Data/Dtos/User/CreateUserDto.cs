using System.ComponentModel.DataAnnotations;

namespace api.Data.Dtos.User;

public class CreateUserDto
{
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
    [Required, Compare(nameof(Password))]
    public string PasswordConfirmation { get; set; }
}
