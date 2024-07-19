using Microsoft.VisualStudio.TestTools.UnitTesting;
using System; 
using System.Linq; 
using FluentAssertions; 
using Moq;
using IPO.Company.Models.API;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using IPO.Company.Models.Configuration;
using System.Text.RegularExpressions;

namespace IPO.Company.UnitTests.Models
{
    [TestClass]
    [TestCategory("CompanyRegistrationNumberAttributeTests")]
    public class CompanyRegistrationNumberAttributeTests
    {
        private readonly CompanyRegistrationNumberAttributeTestsHelper _CompanyRegistrationNumberAttributeTestsHelper; 
        private readonly Mock<IServiceProvider> _mockServiceProvider;
        private readonly ValidationContext  _mockValidationContext; 

        public CompanyRegistrationNumberAttributeTests()
        { 
            this._CompanyRegistrationNumberAttributeTestsHelper = new CompanyRegistrationNumberAttributeTestsHelper();  
            this._mockServiceProvider = new Mock<IServiceProvider>();  
            this._mockValidationContext = new ValidationContext(this, this._mockServiceProvider.Object, null);  
        }

        [TestMethod]
        public void IsValidWhenCompanyNumberIsEmptyReturnsEmptyValidationResultError()
        {
            //Arrange
            var companyNumber = string.Empty; 
            var expectedResult = this._CompanyRegistrationNumberAttributeTestsHelper.CreateEmptyCompaniesHouseNumberValidationResult();

            //Act
            var result = this._CompanyRegistrationNumberAttributeTestsHelper.TestIsValid(companyNumber,(ValidationContext)this._mockValidationContext);

            //Assert 
            result.Should().BeOfType(expectedResult.GetType());
            result.ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        }

        [TestMethod]
        public void IsValidWhenCompanyNumberIsNullReturnsEmptyValidationResultError()
        {
            //Arrange
            var companyNumber = (string)null;
            var expectedResult = this._CompanyRegistrationNumberAttributeTestsHelper.CreateEmptyCompaniesHouseNumberValidationResult();

            //Act
            var result = this._CompanyRegistrationNumberAttributeTestsHelper.TestIsValid(companyNumber, (ValidationContext)this._mockValidationContext);

            //Assert 
            result.Should().BeOfType(expectedResult.GetType());
            result.ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        }

        [TestMethod]
        public void IsValidWhenCompanyNumberIsInvalidReturnsInvalidCompaniesHouseNumberValidationResultError()
        {
            //Arrange
            var companyNumber = "test";
            var expectedResult = this._CompanyRegistrationNumberAttributeTestsHelper.CreateInvalidCompaniesHouseNumberValidationResult(companyNumber);

            //Act
            var result = this._CompanyRegistrationNumberAttributeTestsHelper.TestIsValid(companyNumber, (ValidationContext)this._mockValidationContext);

            //Assert 
            result.Should().BeOfType(expectedResult.GetType());
            result.ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        } 

        [TestMethod]
        public void IsValidWhenCompanyNumberIsValidDigitsOnlyReturnsSuccess()
        {
            //Arrange
            var companyNumber = "12341234";
            var expectedResult = ValidationResult.Success;

            //Act
            var result = this._CompanyRegistrationNumberAttributeTestsHelper.TestIsValid(companyNumber, (ValidationContext)this._mockValidationContext);

            //Assert 
            result.Should().Be(expectedResult); 
        }  

        [TestMethod]
        public void CreateAllDigitsCompanyNumberRegexReturnsValidRegex()
        {
            //Arrange
            var regex = new Regex("^[0-9]{8}$");

            //Act
            var result = this._CompanyRegistrationNumberAttributeTestsHelper.CreateAllDigitsCompanyNumberRegex();

            //Assert 
            result.Should().BeOfType(regex.GetType());
            result.ToString().Should().Be(result.ToString());
        }

        [TestMethod]
        public void CreateDigitsWithAddressCompanyNumberRegexReturnsValidRegex()
        {
            //Arrange
            var regex = new Regex("^[A-Za-z]{2}[A-Za-z0-9]{6}$");

            //Act
            var result = this._CompanyRegistrationNumberAttributeTestsHelper.CreateDigitsWithAddressCompanyNumberRegex();

            //Assert 
            result.Should().BeOfType(regex.GetType());
            result.ToString().Should().Be(result.ToString());
        }

        [TestMethod]
        public void CreateInvalidCompaniesHouseNumberValidationResultReturnsValidValidationResult()
        {
            //Arrange
            var companyNumber = "tt123123";
            var expectedResult = new ValidationResult($"The supplied company number({companyNumber}) is not a valid Companies House number.");

            //Act
            var result = this._CompanyRegistrationNumberAttributeTestsHelper.CreateInvalidCompaniesHouseNumberValidationResult(companyNumber);

            //Assert
            result.Should().BeOfType(expectedResult.GetType());
            result.ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        } 

        [TestMethod]
        public void CreateEmptyCompaniesHouseNumberValidationResultReturnsValidValidationResult()
        {
            //Arrange 
            var expectedResult = new ValidationResult("The supplied company number cannot be null or empty.");

            //Act
            var result = this._CompanyRegistrationNumberAttributeTestsHelper.CreateEmptyCompaniesHouseNumberValidationResult();

            //Assert
            result.Should().BeOfType(expectedResult.GetType());
            result.ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        }
    }
}
