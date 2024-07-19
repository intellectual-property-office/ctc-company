using IPO.Company.Models.API;

namespace IPO.Company.Interfaces
{
    public interface ICompanyManagementService
    {
        Task<CompanyDetailsResult> GetCompanyAddress(CompanyDetailsRequest companyNumber);
    }
}
