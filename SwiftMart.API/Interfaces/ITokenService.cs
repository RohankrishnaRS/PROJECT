using SwiftMart.API.Models;

namespace SwiftMart.API.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
