using FluentAssertions;
using IPO.Company.Models.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace IPO.Company.UnitTests.Models
{
    [TestClass]
    [TestCategory("CompanyRegistrationNumberAttributeTests")]
    public class CompanyRegistrationNumberAttributeTestsHelper : CompanyRegistrationNumberAttribute
    {  

        public ValidationResult TestIsValid(object value, ValidationContext validationContext)
        { 
            return this.IsValid(value,validationContext);
        }

        [TestMethod] 
        public void TestMethodForIsValid()
        {
            //Arrange
            var value = (string)null;
            var validationContext = (ValidationContext)null;
            var expectedResult = this.CreateEmptyCompaniesHouseNumberValidationResult();

            //Act
            var result = this.IsValid(value, validationContext);

            //Assert
            result.Should().BeOfType(expectedResult.GetType());
            result.ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        } 
    }
}
