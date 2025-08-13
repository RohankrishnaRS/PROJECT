using SwiftMart.API.Models;

namespace SwiftMart.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, IList<string> roles);
    }
}
