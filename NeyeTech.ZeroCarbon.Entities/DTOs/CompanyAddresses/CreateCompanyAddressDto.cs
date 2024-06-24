namespace NeyeTech.ZeroCarbon.Entities.DTOs.CompanyAddresses
{
    public class CreateCompanyAddressDto : BaseDto
    {
        public long CompanyId { get; set; }
        public string Name { get; set; }
        public string[] NaceCodes { get; set; }
        public long CountyId { get; set; }
        public string PhoneNumber { get; set; }
        public string NaceKod { get; set; }
        public string Address { get; set; }
        public string TaxAdministration { get; set; }
        public string TaxNumber { get; set; }
        public bool IsSelected { get; set; }
    }
}
