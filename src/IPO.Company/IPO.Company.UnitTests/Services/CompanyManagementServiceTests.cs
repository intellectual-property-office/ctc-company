using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using AutoFixture;
using IPO.Company.Interfaces;
using IPO.Company.Services;
using IPO.Company.Gateways.Models;
using IPO.Company.Models.API;
using IPO.Common.Infrastructure;

namespace IPO.Company.UnitTests.Services
{
    [TestClass]
    public class CompanyManagementServiceTests
    {
        private Fixture _fixture; 
        private Mock<ICompaniesHouseGateway> _mockCompaniesHouseGateway; 

        public CompanyManagementServiceTests()
        {
            this._fixture = new Fixture();
            this._mockCompaniesHouseGateway = new Mock<ICompaniesHouseGateway>(); 

        }

        [TestMethod]
        public void GetCompanyAddressWhenReturnedModelCompanyAddressIsNullThrowsStatusCodeExceptionAsUnprocessedEntity()
        {
            //Arrange
            var companyDetailsRequest = new CompanyDetailsRequest() { CompanyNumber = null };
            var expectedException = StatusCodeExceptionFactory.CreateNotFoundStatusCodeException<ICompaniesHouseGateway>("E003", companyDetailsRequest.CompanyNumber);

            this._mockCompaniesHouseGateway.Setup(o => o.GetCompanyAddressDetails(It.IsAny<string>())).ThrowsAsync(expectedException).Verifiable();
            var companiesManagementService = new CompanyManagementService(this._mockCompaniesHouseGateway.Object);

            //Act
            var getCompanyAddress = async () => await companiesManagementService.GetCompanyAddress(companyDetailsRequest);

            //Assert
            getCompanyAddress.Should().ThrowAsync<StatusCodeException>()
                                    .WithMessage(expectedException.Message)
                                    .WithInnerException(expectedException.Error.GetType())
                                    .WithMessage(expectedException.Error.Description);
            this._mockCompaniesHouseGateway.Verify();

        }
        [TestMethod]
        public async Task GetCompanyAddressWhenRequestModelNullThrowException()
        {
            //Arrange
            var companyDetailsRequest = (CompanyDetailsRequest)null; 
           // this._mockCompaniesHouseGateway.Setup(o => o.GetCompanyAddressDetails(It.IsAny<string>())).ReturnsAsync(expectedResult).Verifiable();
            var companiesManagementService = new CompanyManagementService(this._mockCompaniesHouseGateway.Object);

            //Act
            var getCompanyAddress = async () => await companiesManagementService.GetCompanyAddress(companyDetailsRequest);

            //Assert 
            await getCompanyAddress.Should().ThrowAsync<NullReferenceException>();
            this._mockCompaniesHouseGateway.Verify();
        }
        [TestMethod]
        public async Task GetCompanyAddressReturnsValidModel()
        {
            //Arrange
            var companyDetailsRequest = this._fixture.Create<CompanyDetailsRequest>();
            var expectedResult = this._fixture.Build<CompanyDetailsResult>().With(o => o.CompanyNumber,
                companyDetailsRequest.CompanyNumber).Create();
            this._mockCompaniesHouseGateway.Setup(o => o.GetCompanyAddressDetails(It.IsAny<string>())).ReturnsAsync(expectedResult).Verifiable();
            var companiesManagementService = new CompanyManagementService(this._mockCompaniesHouseGateway.Object);

            //Act
            var getCompanyAddress = await companiesManagementService.GetCompanyAddress(companyDetailsRequest);

            //Assert 
            getCompanyAddress.Should().NotBeNull();
            getCompanyAddress.Should().NotBeOfType(companyDetailsRequest.GetType());
            getCompanyAddress.Should().BeEquivalentTo(companyDetailsRequest);
            this._mockCompaniesHouseGateway.Verify();
        }
    }
}
