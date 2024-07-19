using Microsoft.VisualStudio.TestTools.UnitTesting; 
using System.Threading.Tasks;
using FluentAssertions; 
using Moq;
using System.Net.Http;
using IPO.Company.Gateways;
using AutoFixture;
using IPO.Company.Gateways.Models;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;
using System.Threading;
using IPO.Common.Infrastructure;
using IPO.Company.Interfaces;
using IPO.Company.Models.Configuration;
using System;

namespace IPO.Company.UnitTests.Gateways
{
    [TestClass]
    public class CompaniesHouseGatewayTests
    { 
        private readonly Mock<HttpClient> _httpClient; 
        private readonly Fixture _fixture;
        private readonly Mock<HttpMessageHandler> mockedHttpMessageHandler;
        private CompaniesHouseGateway _companiesHouseGateway;
        private readonly CompaniesHouseApiSettings _companiesHouseApiSettings;
        public CompaniesHouseGatewayTests()
        {  
            this._httpClient = new Mock<HttpClient>(); 
            this._fixture = new Fixture();
            this.mockedHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            this._companiesHouseApiSettings = this._fixture.Create<CompaniesHouseApiSettings>();
        }

        [TestMethod]
        public async Task GetCompanyAddressDetailsAsyncReturnsValidCompanyDetailsResult()
        {
            // Arrange
            var companyNumber = this._fixture.Create<string>();

            var companyResult = this._fixture.Build<CompanyResult>().With(o => o.company_number, companyNumber).Create();
             
            mockedHttpMessageHandler
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = JsonContent.Create(companyResult,companyResult.GetType())
               })
               .Verifiable();

            var client = new HttpClient(mockedHttpMessageHandler.Object) { BaseAddress = this._companiesHouseApiSettings.BaseAddress};

            this._companiesHouseGateway = new CompaniesHouseGateway(client);

            //Act
            var getCompanyAddressDetails = await this._companiesHouseGateway.GetCompanyAddressDetails(companyNumber);

            //Assert
            getCompanyAddressDetails.Should().NotBeNull();
            getCompanyAddressDetails.AddressLine1.Should().Be(companyResult.registered_office_address.address_line_1);
            getCompanyAddressDetails.AddressLine2.Should().Be(companyResult.registered_office_address.address_line_2);
            getCompanyAddressDetails.CompanyName.Should().Be(companyResult.company_name);
            getCompanyAddressDetails.CompanyNumber.Should().Be(companyResult.company_number);
            getCompanyAddressDetails.Country.Should().Be(companyResult.registered_office_address.country);
            getCompanyAddressDetails.Locality.Should().Be(companyResult.registered_office_address.locality);
            getCompanyAddressDetails.PostCode.Should().Be(companyResult.registered_office_address.postal_code);
            getCompanyAddressDetails.PostOfficeBox.Should().Be(companyResult.registered_office_address.po_box);
            getCompanyAddressDetails.Premises.Should().Be(companyResult.registered_office_address.premises);
            getCompanyAddressDetails.Region.Should().Be(companyResult.registered_office_address.region);
            getCompanyAddressDetails.RegisteredOfficeIsInDispute.Should().Be(companyResult.registered_office_is_in_dispute ?? false);
            getCompanyAddressDetails.UndeliverableRegisteredOfficeAddress.Should().Be(companyResult.undeliverable_registered_office_address ?? false);
            getCompanyAddressDetails.DateOfCreation.Should().Be(companyResult.date_of_creation ?? default(DateTime).ToShortDateString());
            getCompanyAddressDetails.IsActive.Should().Be(companyResult.company_status == null ? false : (companyResult.company_status == CompanyStatus.Active));
            mockedHttpMessageHandler.Verify(); 
        }

        [TestMethod]
        public void GetCompanyAddressDetailsAsyncWhenResponseNotFoundThrowsInvalidJsonException()
        {
            // Arrange
            var companyNumber = this._fixture.Create<string>();
            var expectedException = StatusCodeExceptionFactory.CreateNotFoundStatusCodeException<ICompaniesHouseGateway>("E003", companyNumber);

            mockedHttpMessageHandler
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.NotFound,
                   Content = JsonContent.Create(string.Empty)
               })
               .Verifiable();

            var client = new HttpClient(mockedHttpMessageHandler.Object) { BaseAddress = this._companiesHouseApiSettings.BaseAddress };

            this._companiesHouseGateway = new CompaniesHouseGateway(client);

            //Act
            var getCompanyAddressDetails = async () => await this._companiesHouseGateway.GetCompanyAddressDetails(companyNumber);

            //Assert 
            getCompanyAddressDetails.Should().ThrowAsync<StatusCodeException>()
                                    .WithMessage(expectedException.Message)
                                    .WithInnerException(expectedException.Error.GetType())
                                    .WithMessage(expectedException.Error.Description);
            mockedHttpMessageHandler.Verify();
        } 

        [TestMethod]
        public void GetCompanyAddressDetailsAsyncWhenAddressDetailsAreNullThrowsInvalidJsonException()
        {
            // Arrange
            var companyNumber = this._fixture.Create<string>();
            var expectedException = StatusCodeExceptionFactory.CreateNotFoundStatusCodeException<ICompaniesHouseGateway>("E003", companyNumber);
            var companyResult = this._fixture.Build<CompanyResult>().With(o => o.company_number, companyNumber).Create();
            companyResult.registered_office_address = null;

            mockedHttpMessageHandler
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = JsonContent.Create(string.Empty)
               })
               .Verifiable();

            var client = new HttpClient(mockedHttpMessageHandler.Object) { BaseAddress = this._companiesHouseApiSettings.BaseAddress };

            this._companiesHouseGateway = new CompaniesHouseGateway(client);

            //Act
            var getCompanyAddressDetails = async () => await this._companiesHouseGateway.GetCompanyAddressDetails(companyNumber);

            //Assert
            getCompanyAddressDetails.Should().ThrowAsync<StatusCodeException>()
                                    .WithMessage(expectedException.Message)
                                    .WithInnerException(expectedException.Error.GetType())
                                    .WithMessage(expectedException.Error.Description);
            mockedHttpMessageHandler.Verify();
        }
    }
}
