using Microsoft.VisualStudio.TestTools.UnitTesting;
using System; 
using FluentAssertions;  
using AutoFixture;
using IPO.Company.Gateways.Models; 

namespace IPO.Company.UnitTests.Gateways
{
    [TestClass]
    public class CompanyResultsTests
    {
        public readonly Fixture _fixture;
        public CompanyResultsTests()
        {
            this._fixture = new Fixture();
        } 

        [TestMethod]
        public void ToexpectedResultWhenModelAddressIsNullReturnsNull()
        {
            //Arrange
            var companyDetailsModel = this._fixture.Create<CompanyResult>();
            companyDetailsModel.registered_office_address = null; 

            //Act
            var result = companyDetailsModel.ToCompanyDetailsResult();

            //Assert 
            result.Should().Be(null);
        }

        [TestMethod]
        public void ToexpectedResultWhenModelIsNotNullReturnsValidCompanyAddressResultModel()
        {
            //Arrange
            var companyDetailsModel = this._fixture.Create<CompanyResult>(); 

            //Act
            var result = companyDetailsModel.ToCompanyDetailsResult();

            //Assert 
            result.Should().NotBeNull();
            result.AddressLine1.Should().Be(companyDetailsModel.registered_office_address.address_line_1);
            result.AddressLine2.Should().Be(companyDetailsModel.registered_office_address.address_line_2);
            result.CompanyName.Should().Be(companyDetailsModel.company_name);
            result.CompanyNumber.Should().Be(companyDetailsModel.company_number);
            result.Country.Should().Be(companyDetailsModel.registered_office_address.country);
            result.Locality.Should().Be(companyDetailsModel.registered_office_address.locality);
            result.PostCode.Should().Be(companyDetailsModel.registered_office_address.postal_code);
            result.PostOfficeBox.Should().Be(companyDetailsModel.registered_office_address.po_box);
            result.Premises.Should().Be(companyDetailsModel.registered_office_address.premises);
            result.Region.Should().Be(companyDetailsModel.registered_office_address.region);
            result.RegisteredOfficeIsInDispute.Should().Be(companyDetailsModel.registered_office_is_in_dispute ?? false);
            result.UndeliverableRegisteredOfficeAddress.Should().Be(companyDetailsModel.undeliverable_registered_office_address ?? false);
            result.DateOfCreation.Should().Be(companyDetailsModel.date_of_creation ?? default(DateTime).ToShortDateString());
            result.IsActive.Should().Be(companyDetailsModel.company_status == null ? false : (companyDetailsModel.company_status == CompanyStatus.Active));
        }
    }
}
