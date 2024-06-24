namespace NeyeTech.ZeroCarbon.Entities.DTOs.Companies
{
    public class AddCompanyDto
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? CountyId { get; set; }
    }
}
