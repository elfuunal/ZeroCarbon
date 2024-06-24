﻿using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using NeyeTech.ZeroCarbon.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class EmissionSourceScopeRepository : EntityRepositoryBase<EmissionSourceScope, ProjectDbContext>, IEmissionSourceScopeRepository
    {
        ProjectDbContext _context;

        public EmissionSourceScopeRepository(ProjectDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
