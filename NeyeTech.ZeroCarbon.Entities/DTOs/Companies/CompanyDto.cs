using NeyeTech.ZeroCarbon.Entities.DTOs.CompanyAddresses;

namespace NeyeTech.ZeroCarbon.Entities.DTOs.Companies
{
    public class CompanyDto : BaseDto
    {
        public string Title { get; set; }
        public DateTime RecordDate { get; set; }
        public ICollection<CompanyAddressDto> CompanyAddresses { get; set; }
    }
}
