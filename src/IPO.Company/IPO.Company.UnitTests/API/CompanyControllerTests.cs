using Microsoft.VisualStudio.TestTools.UnitTesting; 
using System.Threading.Tasks;
using FluentAssertions; 
using Moq;
using AutoFixture;
using IPO.Company.Interfaces;
using IPO.Company.Models.API;
using IPO.Company.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using IPO.Company.Gateways.Models;
using IPO.Common.Infrastructure;

namespace IPO.Company.UnitTests.API
{
    [TestClass]
    public class CompanyControllerTests
    {
        private readonly Mock<ICompanyManagementService> _mockCompanyManagementService;
        private readonly Fixture _fixture; 

        public CompanyControllerTests()
        {
            this._mockCompanyManagementService = new Mock<ICompanyManagementService>();
            this._fixture = new Fixture(); 
        }

        [TestMethod]
        public async Task GetCompanyAsyncReturnsOk()
        {
            // Arrange
            var companyDetailsRequest = this._fixture.Create<CompanyDetailsRequest>();
            var companyDetailsResult = this._fixture.Build<CompanyDetailsResult>()
                                                      .With(o => o.CompanyNumber, companyDetailsRequest.CompanyNumber)
                                                      .Create();

            this._mockCompanyManagementService.Setup(s => s.GetCompanyAddress(It.IsAny<CompanyDetailsRequest>()))
                                         .ReturnsAsync(companyDetailsResult)
                                         .Verifiable();

            var companyApi = new CompanyController(this._mockCompanyManagementService.Object);

            // Act
            var companyRequestResult = await companyApi.GetCompany(companyDetailsRequest);
            var companyResults = (OkObjectResult)companyRequestResult.Result;
            var results = (CompanyDetailsResult)companyResults.Value;

            // Assert 
            companyResults.StatusCode.Should().NotBeNull();
            companyResults.StatusCode.Should().Be((int)HttpStatusCode.OK);
            results.Should().NotBeNull();
            results.Should().Be(companyDetailsResult); 
            this._mockCompanyManagementService.Verify();
        }

        [TestMethod]
        public void GetCompanyAsyncReturnsUnprocessableEntity()
        {
            // Arrange
            var companyDetailsRequest = this._fixture.Create<CompanyDetailsRequest>();
            var expectedException = StatusCodeExceptionFactory.CreateNotFoundStatusCodeException<ICompaniesHouseGateway>("E003",companyDetailsRequest.CompanyNumber);


            this._mockCompanyManagementService.Setup(s => s.GetCompanyAddress(It.IsAny<CompanyDetailsRequest>()))
                                         .Throws(expectedException)
                                         .Verifiable();

            var companyApi = new CompanyController(this._mockCompanyManagementService.Object);

            // Act
            var companyRequestResult = async () => await companyApi.GetCompany(companyDetailsRequest);

            // Assert  
            companyRequestResult.Should().ThrowAsync<StatusCodeException>()
                                    .WithMessage(expectedException.Message)
                                    .WithInnerException(expectedException.Error.GetType())
                                    .WithMessage(expectedException.Error.Description);
            this._mockCompanyManagementService.Verify();
        }
    }
}
