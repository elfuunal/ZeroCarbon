using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.Core.Extensions;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;
using Microsoft.EntityFrameworkCore;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class CompanyRepository : EntityRepositoryBase<Company, ProjectDbContext>, ICompanyRepository
    {
        ProjectDbContext _context;
        public CompanyRepository(ProjectDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetCompanyListAsync(long userId)
        {
            return await _context.Companies
                    .Where(s => s.UserId == userId)
                    .ToListAsync();
        }

        public async Task<List<Company>> GetAllCompanyWithAddressListAsync()
        {
            return await _context.Companies
                .OrderBy(s => s.Title)
                .ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(long id)
        {

            return await _context.Companies
                    .Where(s => s.Id == id)
                    .FirstOrDefaultAsync();
        }
    }
}
