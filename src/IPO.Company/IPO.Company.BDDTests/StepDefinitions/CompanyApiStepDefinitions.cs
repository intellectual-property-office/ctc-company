using TechTalk.SpecFlow;
using IPO.Company.BDDTests.Helpers;
using Microsoft.AspNetCore.TestHost; 
using Newtonsoft.Json;
using IPO.Company.Models.API;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using TechTalk.SpecFlow.Assist;

namespace IPO.Company.BDDTests.StepDefinitions
{
    [Binding]
    public class CompanyApiStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestServer _server;
        private readonly HttpClient _client; 
        private readonly CompanyRegistrationNumberAttributeTestsHelper _companyRegistrationNumberAttributeHelper;
        private readonly IServiceProvider _serviceProvider;

        public CompanyApiStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _server = TestStartup.GetTestServer(); 
            _client = _server.CreateClient(); 
            _companyRegistrationNumberAttributeHelper = new CompanyRegistrationNumberAttributeTestsHelper();
            _serviceProvider = (IServiceProvider)_server.Services.GetService(typeof(IServiceProvider));
        }

        [Given(@"Requesting company details for every valid type of company number")]
        public void GivenRequestingCompanyDetailsForEveryValidTypeOfCompnyNumber(Table table)
        { 
            var companyNumbers = table.CreateSet<CompanyNumberType>();
            _scenarioContext.Add("CompanyNumbers", companyNumbers);
        }

        [When(@"apiURL / requested")]
        public async Task WhenApiURLRequested()
        {
            var companyNumberTypes = _scenarioContext.Get<IEnumerable<CompanyNumberType>>("CompanyNumbers");
            var companyDetails = new List<CompanyDetailsResponseData>();
            foreach (var companyNumberType in companyNumberTypes)
            {
                var getResponse = await _client.GetAsync($"/?CompanyNumber={companyNumberType.CompanyNumber}");
                var getResponseResult = JsonConvert.DeserializeObject<CompanyDetailsResult>(await getResponse.Content.ReadAsStringAsync());
                companyDetails.Add(new CompanyDetailsResponseData { CompanyNumber = companyNumberType.CompanyNumber, CompanyDetailsResult = getResponseResult });
            }
            _scenarioContext.Add("CompanyDetails", companyDetails);
        }

        [Then(@"the company details are returned succesfully")]
        public void ThenTheCompanyDetailsAreReturnedSuccesfully()
        {
            var companyDetailsData = _scenarioContext.Get<List<CompanyDetailsResponseData>>("CompanyDetails");
            var companyNumberTypes = _scenarioContext.Get<IEnumerable<CompanyNumberType>>("CompanyNumbers");
            foreach (var companyDetails in companyDetailsData)
            {
                companyDetails.Should().NotBeNull();
                companyDetails.CompanyDetailsResult.Should().NotBeNull();
                companyDetails.CompanyNumber.Should().NotBeNull();
                companyNumberTypes.Select(o=>o.CompanyNumber).Should().Contain(companyDetails.CompanyNumber);
                this._companyRegistrationNumberAttributeHelper.TestIsValid(companyDetails.CompanyNumber, new ValidationContext(this, this._serviceProvider, null));
                companyDetails.CompanyDetailsResult.CompanyNumber.Should().NotBeNull();
                companyDetails.CompanyDetailsResult.CompanyNumber.Should().Be(companyDetails.CompanyNumber);
                companyDetails.CompanyDetailsResult.CompanyName.Should().NotBeNull();
                companyDetails.CompanyDetailsResult.PostCode.Should().NotBeNull();
                companyDetails.CompanyDetailsResult.AddressLine1.Should().NotBeNull(); 
            }
        }
    }
}
