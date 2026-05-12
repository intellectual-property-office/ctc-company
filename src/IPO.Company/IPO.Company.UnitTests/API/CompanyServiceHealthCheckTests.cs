using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPO.Company.API.HealthChecks;
using IPO.Company.Interfaces;
using IPO.Company.Models.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IPO.Company.UnitTests.API
{
    [TestClass]
    public class CompanyServiceHealthCheckTests
    {
        private Mock<ICompanyManagementService> _mockCompanyService;
        private IConfiguration _configuration;
        private CompanyServiceHealthCheck _uut;
        private CompanyDetailsResult _result;
        private string _companyNumber;

        [TestInitialize]
        public void TestInitialize()
        {
            _companyNumber = "123";

            _mockCompanyService = new Mock<ICompanyManagementService>();

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string>
                    {
                        { "HealthReady:CompanyNumbers", _companyNumber }
                    }
                 )
                .Build();

            _uut = new CompanyServiceHealthCheck(_mockCompanyService.Object, _configuration);
        }

        [TestMethod]
        public async Task ReadyReturnsAvailableIfCompanyFound()
        {
            // Arrange

            _result = new CompanyDetailsResult
            {
                CompanyNumber = _companyNumber,
                CompanyName = "A.C.M.E"
            };

            _mockCompanyService.Setup(e => e.GetCompanyAddress(It.IsAny<CompanyDetailsRequest>())).ReturnsAsync(_result).Verifiable();

            // Act

            var actual = await _uut.CheckHealthAsync(null, CancellationToken.None);

            // Assert

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(HealthCheckResult));
            Assert.AreEqual("Company provider is available", actual.Description);
            Assert.AreEqual(HealthStatus.Healthy, actual.Status);
            Assert.IsNull(actual.Exception);
            _mockCompanyService.Verify();
        }

        [TestMethod]
        public async Task ReadyReturnsUnavailableIfCompanyNotFound()
        {
            // Arrange

            _mockCompanyService.Setup(e => e.GetCompanyAddress(It.IsAny<CompanyDetailsRequest>())).ReturnsAsync(_result).Verifiable();

            // Act

            var actual = await _uut.CheckHealthAsync(null, CancellationToken.None);

            // Assert

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(HealthCheckResult));
            Assert.AreEqual("Company provider is not available", actual.Description);
            Assert.AreEqual(HealthStatus.Unhealthy, actual.Status);
            Assert.IsNull(actual.Exception);
            _mockCompanyService.Verify();
        }
    }
}