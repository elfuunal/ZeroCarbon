using NeyeTech.ZeroCarbon.Core.DataAccess;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.DataAccess.Abstract
{
    public interface IGroupClaimRepository : IEntityRepository<GroupClaim>
    {
        Task BulkInsert(long groupId, IEnumerable<GroupClaim> groupClaims);
    }
}
