using NeyeTech.ZeroCarbon.Core.DataAccess;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyData;

namespace NeyeTech.ZeroCarbon.DataAccess.Abstract
{
    public interface ICompanyDataRepository : IEntityRepository<CompanyData>
    {
        Task<List<CompanyData>> GetCompanyDataAsync(CompanyDataDto model);
        Task SaveCompanyDataAsync(List<CompanyData> models);
        Task DeleteCompanyDataAsync(long dataId);
        Task<List<CompanyData>> GetReportCompanyDataAsync(long companyAddresId, int year);
        Task<List<CompanyData>> GetReportCompanyDataAsync(long companyAddresId, int year, long kapsam);
        Task<List<CompanyData>> GetReportCompanyDataAsync(long companyAddresId);
        Task<List<CompanyData>> GetCompanyEmissionSourcesAsync(long companyAddressId);
        Task<List<CompanyData>> GetCompanyDataWithoutMonthAsync(CompanyDataDto model);
        Task<List<CompanyData>> GetReportCompanyDataWithoutYearAsync(long companyAddresId);
        Task<List<CompanyData>> GetReportCompanyDataWithYearAsync(long companyAddresId, int year);
    }
}
