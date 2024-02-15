﻿using System.ComponentModel.DataAnnotations;

namespace api.Data.Dtos.User;

public class LoginUserDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
