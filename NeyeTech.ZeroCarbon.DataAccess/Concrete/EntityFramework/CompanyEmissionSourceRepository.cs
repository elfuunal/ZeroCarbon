using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyEmissionSources;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class CompanyEmissionSourceRepository : EntityRepositoryBase<CompanyEmissionSource, ProjectDbContext>, ICompanyEmissionSourceRepository
    {
        ProjectDbContext _context;
        public CompanyEmissionSourceRepository(ProjectDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<CompanyEmissionSource>> GetCompanyEmissionSources(long companyId)
        {
            return await _context.CompanyEmissionSources
                .Include(s => s.EmissionSource)
                .Where(s => s.CompanyId == companyId)
                .ToListAsync();

        }

        public async Task<List<CompanyEmissionSource>> GetCompanyEmissionSourcesWithCompanyId(long companyId)
        {
            return await _context.CompanyEmissionSources
                .Include(s => s.EmissionSource)
                .Where(s => s.CompanyId == companyId)
                .ToListAsync();

        }

        public async Task SaveCompanyEmissionSources(AddCompanyEmissionSourceDto model)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var list = await _context.CompanyEmissionSources.Where(s => s.CompanyId == model.CompanyId)
                    .ToListAsync();

                _context.CompanyEmissionSources.RemoveRange(list);
                await _context.SaveChangesAsync();

                List<CompanyEmissionSource> addList = new List<CompanyEmissionSource>();

                foreach (long item in model.Kalemler)
                {
                    CompanyEmissionSource temp = new CompanyEmissionSource()
                    {
                        CompanyId = model.CompanyId,
                        EmissionSourceId = item
                    };

                    addList.Add(temp);
                }

                _context.CompanyEmissionSources.AddRange(addList);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }


        }
    }
}
