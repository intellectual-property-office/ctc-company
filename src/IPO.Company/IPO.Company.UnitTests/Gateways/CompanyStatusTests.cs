using Microsoft.VisualStudio.TestTools.UnitTesting;
using System; 
using FluentAssertions; 
using IPO.Company.Gateways.Models;
using System.Runtime.Serialization; 
using System.Text.Json; 

namespace IPO.Company.UnitTests.Gateways
{ 
    [TestClass]
    public class CompanyStatusTests
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public CompanyStatusTests()
        {
            this._jsonSerializerOptions = new JsonSerializerOptions();
            this._jsonSerializerOptions.Converters.Add(new JsonStringEnumWithHyphenCustomNamesConverter<CompanyStatus>());
        } 

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsActiveReturnsRightOption() 
        {
            // Arrange
            string value = "active";  
            var expectedValue = CompanyStatus.Active;

            //Act 
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions); 

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsDissolvedReturnsRightOption() 
        { 
            //Arrange 
            string value = "dissolved";
            var expectedValue = CompanyStatus.Dissolved;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsLiquidationReturnsRightOption() 
        {
            //Arrange
            string value = "liquidation";
            var expectedValue = CompanyStatus.Liquidation;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsReceiverShipReturnsRightOption() 
        { 
            //Arrange 
            string value = "receivership";
            var expectedValue = CompanyStatus.ReceiverShip;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsConvertedClosedReturnsRightOption() 
        { 
            //Arrange 
            string value = "converted-closed"; 
            var expectedValue = CompanyStatus.ConvertedClosed;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsVoluntaryArrangementReturnsRightOption() 
        { 
            //Arrange 
            string value = "voluntary-arrangement"; 
            var expectedValue = CompanyStatus.VoluntaryArrangement;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsInsolvencyProceedingsReturnsRightOption() 
        { 
            //Arrange
            string value = "insolvency-proceedings"; 
            var expectedValue = CompanyStatus.InsolvencyProceedings;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsAdministrationReturnsRightOption() 
        { 
            //Arrange 
            string value = "administration"; 
            var expectedValue = CompanyStatus.Administration;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsOpenReturnsRightOption() 
        { 
            //Arrange 
            string value = "open"; 
            var expectedValue = CompanyStatus.Open;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsClosedReturnsRightOption() 
        { 
            //Arrange 
            string value = "closed"; 
            var expectedValue = CompanyStatus.Closed;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsRegisteredReturnsRightOption() 
        { 
            //Arrange 
            string value = "registered"; 
            var expectedValue = CompanyStatus.Registered;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsRemovedReturnsRightOption() 
        { 
            //Arrange 
            string value = "removed"; 
            var expectedValue = CompanyStatus.Removed;

            //Act
            var result = JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions);

            //Assert
            result.Should().Be(expectedValue);
        }

        [TestMethod]
        public void CompanyStatusEnumWhenValueIsInvalidThrowsInvalidCatException()
        {
            //Arrange 
            string value = Guid.NewGuid().ToString();

            //Act 
            var action = () => JsonSerializer.Deserialize<CompanyStatus>($"\"{value}\"",this._jsonSerializerOptions); 

            //Assert 
            action.Should().Throw<InvalidDataContractException>();

        }

    }
}
