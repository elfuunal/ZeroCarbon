using NeyeTech.ZeroCarbon.Core.Entities;

namespace NeyeTech.ZeroCarbon.Entities.Concrete
{
    public class CompanyEmissionSource : BaseEntity
    {
        public long CompanyId { get; set; }
        public long EmissionSourceId { get; set; }
        public EmissionSource EmissionSource { get; set; }
        public Company Company { get; set; }
    }
}
