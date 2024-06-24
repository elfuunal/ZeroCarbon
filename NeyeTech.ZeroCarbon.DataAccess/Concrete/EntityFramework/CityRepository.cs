using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using NeyeTech.ZeroCarbon.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class CityRepository : EntityRepositoryBase<City, ProjectDbContext>, ICityRepository
    {
        ProjectDbContext _context;
        public CityRepository(ProjectDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
