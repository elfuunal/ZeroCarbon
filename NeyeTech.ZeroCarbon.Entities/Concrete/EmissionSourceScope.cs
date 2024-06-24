using NeyeTech.ZeroCarbon.Core.Entities;

namespace NeyeTech.ZeroCarbon.Entities.Concrete
{
    public class EmissionSourceScope : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public ICollection<EmissionSource> EmissionSources { get; set; }
    }
}
