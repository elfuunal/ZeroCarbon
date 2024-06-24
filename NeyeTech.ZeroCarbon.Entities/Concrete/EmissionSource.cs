using NeyeTech.ZeroCarbon.Core.Entities;

namespace NeyeTech.ZeroCarbon.Entities.Concrete
{
    public class EmissionSource : BaseEntity
    {
        public long EmissionSourceScopeId { get; set; }
        public string GroupCode { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal? CalorificBasic { get; set; }
        public string CalorificBasicUnit { get; set; }
        public decimal? CO2 { get; set; }
        public string CO2Unit { get; set; }
        public decimal? CH4 { get; set; }
        public string CH4Unit { get; set; }
        public decimal? N2O { get; set; }
        public string N2OUnit { get; set; }
        public decimal? Density { get; set; }
        public string DensityUnit { get; set; }
        public decimal? KIP { get; set; }
        public int? WeightOfCargo { get; set; }
        public int? NumberOfRoom { get; set; }
        public int? NumberOfPeopleTraveling { get; set; }
        public bool Status { get; set; }
        public EmissionSourceScope EmissionSourceScope { get; set; }
        public ICollection<CompanyEmissionSource> CompanyEmissionSources { get; set; }
        public ICollection<CompanyData> CompanyDatas { get; set; }
    }
}
