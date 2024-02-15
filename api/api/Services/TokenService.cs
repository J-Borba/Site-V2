using api.Common.Settings;
using api.Data.Models;
using api.Services.Enumerators;
using api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly AppSettings _appSettings;

    public TokenService(IConfiguration configuration, AppSettings appSettings)
    {
        _configuration = configuration;
        _appSettings = appSettings;
    }

    public string GetToken(User user)
    {
        var claims = new Claim[]
        {
            new(nameof(UserPropsEnum.Id), user.Id),
            new(nameof(UserPropsEnum.UserName), user.UserName),
            new(nameof(UserPropsEnum.Email), user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secrets.SymmetricSecurityKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}