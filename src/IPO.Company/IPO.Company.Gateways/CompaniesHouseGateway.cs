using IPO.Company.Interfaces; 
using IPO.Company.Models.API; 
using System.Text.Json;
using IPO.Company.Gateways.Models; 
using System.Net;

namespace IPO.Company.Gateways
{
    public class CompaniesHouseGateway : ICompaniesHouseGateway
    {
        private HttpClient _httpClient { get; }

        public CompaniesHouseGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CompanyDetailsResult> GetCompanyAddressDetails(string companyAddress)
        { 
            var companyAddressResponse = await this._httpClient.GetAsync($"{this._httpClient.BaseAddress}/{companyAddress.Trim().ToUpperInvariant()}");

            if (companyAddressResponse.StatusCode == HttpStatusCode.NotFound)
                throw StatusCodeExceptionFactory.CreateNotFoundStatusCodeException<ICompaniesHouseGateway>("E003",companyAddress);

            var companyAddressResponseData = JsonSerializer.Deserialize<CompanyResult>(await companyAddressResponse.Content.ReadAsStringAsync());
            var companyAddressDetailsResponseData = companyAddressResponseData.ToCompanyDetailsResult();

            if (companyAddressDetailsResponseData == null)
                throw StatusCodeExceptionFactory.CreateNotFoundStatusCodeException<ICompaniesHouseGateway>("E003", companyAddress);

            return companyAddressDetailsResponseData;
        }
         
    }
}
