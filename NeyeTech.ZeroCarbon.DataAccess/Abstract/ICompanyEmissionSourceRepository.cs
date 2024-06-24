using NeyeTech.ZeroCarbon.Core.DataAccess;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyEmissionSources;

namespace NeyeTech.ZeroCarbon.DataAccess.Abstract
{
    public interface ICompanyEmissionSourceRepository : IEntityRepository<CompanyEmissionSource>
    {
        Task<List<CompanyEmissionSource>> GetCompanyEmissionSources(long companyId);
        Task SaveCompanyEmissionSources(AddCompanyEmissionSourceDto model);
        Task<List<CompanyEmissionSource>> GetCompanyEmissionSourcesWithCompanyId(long companyId);
    }
}
