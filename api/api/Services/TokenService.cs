using api.Data.Dtos.Common;
using api.Models;
using api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly AppSettingsDto _appSettings;

    public TokenService(IConfiguration configuration, AppSettingsDto appSettings)
    {
        _configuration = configuration;
        _appSettings = appSettings;
    }

    public string GetToken(User user)
    {
        var claims = new Claim[]
        {
            new("id", user.Id),
            new("username", user.UserName)
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