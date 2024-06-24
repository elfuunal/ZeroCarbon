namespace NeyeTech.ZeroCarbon.Entities.DTOs.CompanyEmissionSources
{
    public class AddCompanyEmissionSourceDto
    {
        public long CompanyId { get; set; }
        public long EmissionSourceId { get; set; }
        public List<long> Kalemler { get; set; }
    }
}
