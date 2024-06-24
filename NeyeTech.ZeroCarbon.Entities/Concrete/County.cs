using NeyeTech.ZeroCarbon.Core.Entities;

namespace NeyeTech.ZeroCarbon.Entities.Concrete
{
    public class County : IEntity
    {
        public long Id { get; set; }
        public long CityId { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
    }
}
