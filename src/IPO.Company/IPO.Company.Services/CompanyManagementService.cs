using IPO.Company.Interfaces;
using IPO.Company.Models.API; 

namespace IPO.Company.Services
{
    public class CompanyManagementService : ICompanyManagementService
    {
        private readonly ICompaniesHouseGateway _companyManagementService;

        public CompanyManagementService(ICompaniesHouseGateway companiesHouseGateway)
        {
            _companyManagementService = companiesHouseGateway;
        }

        public virtual async Task<CompanyDetailsResult> GetCompanyAddress(CompanyDetailsRequest companyDetailsRequest)
        {
            var companyDetailsResult = await _companyManagementService.GetCompanyAddressDetails(companyDetailsRequest!.CompanyNumber!);
            return companyDetailsResult;
        }        
    }
}