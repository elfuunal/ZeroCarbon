using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class UserGroupRepository : EntityRepositoryBase<UserGroup, ProjectDbContext>, IUserGroupRepository
    {
        public UserGroupRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task BulkInsert(long userId, IEnumerable<UserGroup> userGroups)
        {
            var DbUserGroupList = Context.UserGroups.Where(x => x.UserId == userId);

            Context.UserGroups.RemoveRange(DbUserGroupList);
            await Context.UserGroups.AddRangeAsync(userGroups);
        }

        public async Task BulkInsertByGroupId(long groupId, IEnumerable<UserGroup> userGroups)
        {
            var DbUserGroupList = Context.UserGroups.Where(x => x.GroupId == groupId);

            Context.UserGroups.RemoveRange(DbUserGroupList);
            await Context.UserGroups.AddRangeAsync(userGroups);
        }
    }
}
