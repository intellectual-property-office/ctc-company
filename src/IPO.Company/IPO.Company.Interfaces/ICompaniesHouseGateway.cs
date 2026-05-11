using IPO.Company.Models.API;

namespace IPO.Company.Interfaces
{
    public interface ICompaniesHouseGateway
    {
        Task<CompanyDetailsResult> GetCompanyAddressDetails(string companyAddress);
    }
}
