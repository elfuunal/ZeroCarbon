using NeyeTech.ZeroCarbon.Core.Entities;

namespace NeyeTech.ZeroCarbon.Entities.Concrete
{
    public class City : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<County> Counties { get; set; }
    }
}
