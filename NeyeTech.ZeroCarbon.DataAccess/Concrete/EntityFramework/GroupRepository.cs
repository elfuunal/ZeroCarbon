using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class GroupRepository : EntityRepositoryBase<Group, ProjectDbContext>, IGroupRepository
    {
        public GroupRepository(ProjectDbContext context)
            : base(context)
        {
        }
    }
}
