namespace IPO.Company.Models.API
{
    public class CompanyDetailsRequest
    {
        [CompanyRegistrationNumber]
        public string CompanyNumber { get; set; }
    }
}