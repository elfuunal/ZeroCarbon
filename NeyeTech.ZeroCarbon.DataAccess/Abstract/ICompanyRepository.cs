using NeyeTech.ZeroCarbon.Core.DataAccess;
using NeyeTech.ZeroCarbon.Core.Utilities.Results;
using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.Companies;

namespace NeyeTech.ZeroCarbon.DataAccess.Abstract
{
    public interface ICompanyRepository : IEntityRepository<Company>
    {
        Task<List<Company>> GetCompanyListAsync(long userId);
        Task<List<Company>> GetAllCompanyWithAddressListAsync();
        Task<Company> GetCompanyAsync(long id);
    }
}
