using System.ComponentModel.DataAnnotations;

namespace api.Data.Dtos.User;

public class UpdateUserDto
{
    public string NewUserName { get; set; }
    public string NewEmail { get; set; }
}
