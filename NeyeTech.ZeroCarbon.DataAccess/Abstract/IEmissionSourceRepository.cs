using NeyeTech.ZeroCarbon.Core.DataAccess;
using NeyeTech.ZeroCarbon.Entities.Concrete;

namespace NeyeTech.ZeroCarbon.DataAccess.Abstract
{
    public interface IEmissionSourceRepository : IEntityRepository<EmissionSource>
    {
        Task<List<EmissionSource>> GetEmissionSourceListAsync(long scopeId);
    }
}
