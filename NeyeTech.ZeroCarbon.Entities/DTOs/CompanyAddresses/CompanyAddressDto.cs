using NeyeTech.ZeroCarbon.Entities.DTOs.Counties;

namespace NeyeTech.ZeroCarbon.Entities.DTOs.CompanyAddresses
{
    public class CompanyAddressDto : BaseDto
    {
        public long CompanyId { get; set; }
        public long CountyId { get; set; }
        public string Address { get; set; }
        public string TaxNumber { get; set; }
        public string TaxAdministration { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public bool Status { get; set; }
        public bool Selected { get; set; }
        public CountyDto County { get; set; }
    }
}
