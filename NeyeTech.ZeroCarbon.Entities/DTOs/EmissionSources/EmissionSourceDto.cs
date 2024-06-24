using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSourceScopes;

namespace NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSources
{
    public class EmissionSourceDto : BaseDto
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string GroupCode { get; set; }
        public string Unit { get; set; }
        public int? WeightOfCargo { get; set; }
        public int? NumberOfRoom { get; set; }
        public int? NumberOfPeopleTraveling { get; set; }
    }
}