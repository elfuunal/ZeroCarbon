using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class GroupClaimRepository : EntityRepositoryBase<GroupClaim, ProjectDbContext>, IGroupClaimRepository
    {
        public GroupClaimRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public async Task BulkInsert(long groupId, IEnumerable<GroupClaim> groupClaims)
        {
            var DbList = await Context.GroupClaims.Where(x => x.GroupId == groupId).ToListAsync();
            Context.GroupClaims.RemoveRange(DbList);
            await Context.GroupClaims.AddRangeAsync(groupClaims);
        }
    }
}
