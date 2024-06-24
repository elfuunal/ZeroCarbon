using NeyeTech.ZeroCarbon.Core.Entities;

namespace NeyeTech.ZeroCarbon.Entities.Concrete
{
    public class CompanyData : BaseEntity
    {
        public long CompanyId { get; set; }
        public long EmissionSourceId { get; set; }
        public decimal Value { get; set; }
        public int? KisiSayisi { get; set; }
        public int? UcakKapasitesi { get; set; }
        public int? OdaSayisi { get; set; }
        public int? YukAgirligi { get; set; }
        public EmissionSource EmissionSource { get; set; }
        public Company Company { get; set; }
    }
}
