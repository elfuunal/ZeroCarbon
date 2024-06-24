namespace NeyeTech.ZeroCarbon.Entities.DTOs.Companies
{
    public class CompanyCarbonEmissionDto
    {
        public long Id { get; set; }
        public string EmissionSourceName { get; set; }
        public long ScopeId { get; set; }
        public long EmissionScopeId { get; set; }
        public long EmissionSourceId { get; set; }
        public string EmissionScopeName { get; set; }
        public string FaaliyetVerisi { get; set; }
        public string CO2 { get; set; }
        public string CH4 { get; set; }
        public string N2O { get; set; }
        public string Density { get; set; }
        public string KIP { get; set; }
        public string CalorificBasis { get; set; }
        public string Toplam { get; set; }
        public decimal? Total { get; set; }
    }
}
