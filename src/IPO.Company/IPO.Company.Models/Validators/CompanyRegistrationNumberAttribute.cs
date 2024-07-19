using IPO.Company.Models.Configuration;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IPO.Company.Models.API
{
    public class CompanyRegistrationNumberAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace((string)value))
                return CreateEmptyCompaniesHouseNumberValidationResult(); 

            value = ((string)value).Trim();

            if (CreateAllDigitsCompanyNumberRegex().IsMatch((string)value))
                return ValidationResult.Success;
            else if (CreateDigitsWithAddressCompanyNumberRegex().IsMatch((string)value))
            {  
                return ValidationResult.Success;
            }
            else
                return CreateInvalidCompaniesHouseNumberValidationResult((string)value);
        }

        public bool IsValidCompaniesHouseAddressPrefix(IEnumerable<string> addressPrefixes, string companyNumber)
        {
            string addressPrefix = companyNumber.Substring(0, 2).ToUpperInvariant();
            return addressPrefixes.Contains(addressPrefix);
        }
        public Regex CreateAllDigitsCompanyNumberRegex()
        {
            return new Regex("^[0-9]{8}$");
        }
        public Regex CreateDigitsWithAddressCompanyNumberRegex()
        {
            return new Regex("^[A-Za-z]{2}[A-Za-z0-9]{6}$");
        }
        public ValidationResult CreateInvalidCompaniesHouseNumberValidationResult(string value)
        {
            return new ValidationResult($"The supplied company number({value}) is not a valid Companies House number.");
        } 
        public ValidationResult CreateEmptyCompaniesHouseNumberValidationResult()
        {
            return new ValidationResult("The supplied company number cannot be null or empty.");
        }

    }
}