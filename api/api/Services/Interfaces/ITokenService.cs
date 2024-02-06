using api.Models;

namespace api.Services.Interfaces;

public interface ITokenService
{
    public string GetToken(User user);
}
