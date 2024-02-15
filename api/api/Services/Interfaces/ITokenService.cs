using api.Data.Models;

namespace api.Services.Interfaces;

public interface ITokenService
{
    string GetToken(User user);
}
