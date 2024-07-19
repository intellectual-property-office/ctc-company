using IPO.Company.Models.API; 
using System.ComponentModel.DataAnnotations;

namespace IPO.Company.BDDTests
{ 
    public class CompanyRegistrationNumberAttributeTestsHelper : CompanyRegistrationNumberAttribute
    {  

        public ValidationResult TestIsValid(object value, ValidationContext validationContext)
        { 
            return this.IsValid(value,validationContext);
        }
 
    }
}
