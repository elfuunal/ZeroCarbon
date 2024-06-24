using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSources;

namespace NeyeTech.ZeroCarbon.Entities.DTOs.CompanyData
{
    public class CompanyDataListDto
    {
        public long CompanyId { get; set; }
        public long Scope { get; set; }
        public List<EmissionSourceDto> EmissionSourceDtos { get; set; }
        public List<CompanyDataDto> CompanyDatas { get; set; }
    }
}
