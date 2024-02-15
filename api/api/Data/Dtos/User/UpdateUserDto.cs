using System.ComponentModel.DataAnnotations;

namespace api.Data.Dtos.User;

public class UpdateUserDto
{
    [Required]
    public string CurrentEmail { get; set; }
    public string NewUserName { get; set; }
    public string NewEmail { get; set; }
}
