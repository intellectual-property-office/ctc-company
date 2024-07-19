using IPO.Company.Interfaces;
using IPO.Company.Models.API;
using AutoFixture;

namespace IPO.Company.BDDTests.Helpers
{
    public class MockedCompaniesHouseGateway : ICompaniesHouseGateway
    {
        private readonly Fixture fixture = new Fixture();
        public Task<CompanyDetailsResult> GetCompanyAddressDetails(string companyAddress)
        {
            return Task.FromResult<CompanyDetailsResult>(fixture.Build<CompanyDetailsResult>().With(o=>o.CompanyNumber,companyAddress).Create());
        }
    }
}
