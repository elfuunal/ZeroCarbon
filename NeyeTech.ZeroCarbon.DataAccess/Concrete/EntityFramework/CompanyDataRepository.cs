using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyData;
using System.ComponentModel.Design;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class CompanyDataRepository : EntityRepositoryBase<CompanyData, ProjectDbContext>, ICompanyDataRepository
    {
        ProjectDbContext _context;
        public CompanyDataRepository(ProjectDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<CompanyData>> GetCompanyEmissionSourcesAsync(long companyId)
        {
            return await _context.CompanyDatas
                .Include(s => s.EmissionSource)
            .Where(s =>
                    s.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<List<CompanyData>> GetCompanyDataAsync(CompanyDataDto model)
        {
            return await _context.CompanyDatas
                    .Include(s => s.EmissionSource)
                    .Where(s=>s.CompanyId == model.CompanyId)
                    .ToListAsync();
        }

        public async Task<List<CompanyData>> GetCompanyDataWithoutMonthAsync(CompanyDataDto model)
        {
            return await _context.CompanyDatas
                    .Include(s => s.EmissionSource)
                    .ToListAsync();
        }

        public async Task<List<CompanyData>> GetReportCompanyDataAsync(long companyAddresId, int year)
        {
            return await _context.CompanyDatas
                .Include(s => s.EmissionSource)
                .ToListAsync();
        }

        public async Task<List<CompanyData>> GetReportCompanyDataWithoutYearAsync(long companyId)
        {
            return await _context.CompanyDatas
                .Include(s => s.EmissionSource)
                .Where(s =>
                    s.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<List<CompanyData>> GetReportCompanyDataWithYearAsync(long companyId, int year)
        {
            return await _context.CompanyDatas
                .Include(s => s.EmissionSource)
                .Where(s =>
                    s.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<List<CompanyData>> GetReportCompanyDataAsync(long companyId, int year, long kapsam)
        {
            return await _context.CompanyDatas
                .Include(s => s.EmissionSource)
                .Where(s =>
                    s.CompanyId == companyId && s.EmissionSource.EmissionSourceScopeId == kapsam)
                .ToListAsync();
        }

        public async Task<List<CompanyData>> GetReportCompanyDataAsync(long companyId)
        {
            return await _context.CompanyDatas
                .Include(s => s.EmissionSource)
                .Where(s =>
                    s.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task SaveCompanyDataAsync(List<CompanyData> models)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    List<CompanyData> deletedList = new List<CompanyData>();

                    foreach (var item in models)
                    {
                        var list = await _context.CompanyDatas
                            .Where(s =>
                               s.EmissionSourceId == item.EmissionSourceId)
                            .ToListAsync();

                        deletedList.AddRange(list);
                    }

                    _context.RemoveRange(deletedList);
                    await _context.SaveChangesAsync();

                    foreach (var item in models)
                    {
                        CompanyData companyData = new CompanyData()
                        {
                            CompanyId = item.CompanyId,
                            EmissionSourceId = item.EmissionSourceId,
                            Value = item.Value,
                            KisiSayisi = item.KisiSayisi,
                            UcakKapasitesi = item.UcakKapasitesi,
                            OdaSayisi = item.OdaSayisi,
                            YukAgirligi = item.YukAgirligi
                        };

                        _context.CompanyDatas.Add(companyData);
                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task DeleteCompanyDataAsync(long dataId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var companyData = await _context.CompanyDatas.FirstAsync(s => s.Id == dataId);

                _context.CompanyDatas.Remove(companyData);
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
