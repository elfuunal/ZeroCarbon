using NeyeTech.ZeroCarbon.Core.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        TAccessToken CreateToken<TAccessToken>(User user)

          where TAccessToken : IAccessToken, new();

        string GenerateRefreshToken();
    }
}
