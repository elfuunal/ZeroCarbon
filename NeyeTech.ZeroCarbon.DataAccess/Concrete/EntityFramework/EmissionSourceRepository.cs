using Microsoft.EntityFrameworkCore;
using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class EmissionSourceRepository : EntityRepositoryBase<EmissionSource, ProjectDbContext>, IEmissionSourceRepository
    {
        ProjectDbContext _context;
        public EmissionSourceRepository(ProjectDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<EmissionSource>> GetEmissionSourceListAsync(long scopeId)
        {
            var result = await _context.EmissionSources
                .Include(s => s.EmissionSourceScope)
                .Where(s => s.EmissionSourceScopeId == scopeId)
                .ToListAsync();

            return result;
        }
    }
}
