using System.ComponentModel;

namespace api.Services.Enumerators;

internal enum UserPropsEnum
{
    [Description("Id")]
    Id,
    [Description("UserName")]
    UserName,
    [Description("Email")]
    Email,
    [Description("Password")]
    Password
}