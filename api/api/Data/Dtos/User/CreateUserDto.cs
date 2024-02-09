using System.ComponentModel.DataAnnotations;

namespace api.Data.Dtos.User;

public class CreateUserDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Required, Compare(nameof(Password))]
    public string PasswordConfirmation { get; set; } = string.Empty;
}
