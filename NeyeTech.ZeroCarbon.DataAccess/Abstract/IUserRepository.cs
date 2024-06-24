using NeyeTech.ZeroCarbon.Core.DataAccess;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(long userId);
        Task<User> GetByRefreshToken(string refreshToken);
    }
}
