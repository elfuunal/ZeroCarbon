using NeyeTech.ZeroCarbon.Core.DataAccess;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.DataAccess.Abstract
{
    public interface IUserGroupRepository : IEntityRepository<UserGroup>
    {
        Task BulkInsert(long userId, IEnumerable<UserGroup> userGroups);
        Task BulkInsertByGroupId(long groupId, IEnumerable<UserGroup> userGroups);
    }
}
