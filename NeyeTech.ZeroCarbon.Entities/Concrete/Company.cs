using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.Core.Entities;

namespace NeyeTech.ZeroCarbon.Entities.Concrete
{
    public class Company : BaseEntity
    {
        public long UserId { get; set; }
        public int CountyId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public ICollection<CompanyEmissionSource> CompanyEmissionSources { get; set; }
        public ICollection<CompanyData> CompanyDatas { get; set; }
    }
}