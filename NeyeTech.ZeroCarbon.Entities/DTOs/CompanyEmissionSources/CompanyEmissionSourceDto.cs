using NeyeTech.ZeroCarbon.Entities.Concrete;
using NeyeTech.ZeroCarbon.Entities.DTOs.EmissionSources;

namespace NeyeTech.ZeroCarbon.Entities.DTOs.CompanyEmissionSources
{
    public class CompanyEmissionSourceDto
    {
        public long Id { get; set; }
        public long EmissionSourceId { get; set; }
        public EmissionSourceDto EmissionSource { get; set; }
        public string RecordUsername { get; set; }
        public DateTime RecordDate { get; set; }

        public string RecordDateString
        {
            get
            {
                return RecordDate.ToString("dd.MM.yyyy HH:mm:ss");
            }
            private set { }
        }
    }
}
