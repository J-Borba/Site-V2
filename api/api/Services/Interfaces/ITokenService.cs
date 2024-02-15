using api.Data.Models;

namespace api.Services.Interfaces;

public interface ITokenService
{
    public string GetToken(User user);
}
