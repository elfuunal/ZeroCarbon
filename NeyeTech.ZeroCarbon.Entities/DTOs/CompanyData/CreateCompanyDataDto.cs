namespace NeyeTech.ZeroCarbon.Entities.DTOs.CompanyData
{
    public class CreateCompanyDataDto
    {
        public string NaceCode { get; set; }
        public long CompanyAddressId { get; set; }
        public long EmissionSourceId { get; set; }
        public long CompanyGasmeterId { get; set; }
        public long EmissionSourceScopeId { get; set; }
        //public long UnitCodeId { get; set; }
        public decimal Value { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string FileType { get; set; }
        public int KisiSayisi { get; set; }
        public int? OdaSayisi { get; set; }
        public int? YukAgirligi { get; set; }
        public int UcakKapasitesi { get; set; }
    }
}
