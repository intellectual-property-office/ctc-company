using IPO.Company.Models.API;
using System.Text.Json.Serialization;

namespace IPO.Company.Gateways.Models
{

    public class CompanyResult
    {  
        public bool? registered_office_is_in_dispute { get; set; }
        public string company_name { get; set; }
        public string company_number { get; set; } 
        [JsonConverter(typeof(JsonStringEnumWithHyphenCustomNamesConverter<CompanyStatus>))]
        public CompanyStatus? company_status { get; set; }

        public string date_of_cessation { get; set; }
        public string date_of_creation { get; set; } 
        public bool? undeliverable_registered_office_address { get; set; }
        public OfficeAddress registered_office_address { get; set; }

        public  CompanyDetailsResult ToCompanyDetailsResult()
        {
            var companyDetailsResult = new CompanyDetailsResult();

            if (this == null || this.registered_office_address == null)
                return null;

            companyDetailsResult.CompanyName = this.company_name;
            companyDetailsResult.CompanyNumber = this.company_number;

            companyDetailsResult.AddressLine1 = this.registered_office_address.address_line_1;
            companyDetailsResult.AddressLine2 = this.registered_office_address.address_line_2;
            companyDetailsResult.Country = this.registered_office_address.country;
            companyDetailsResult.Locality = this.registered_office_address.locality;
            companyDetailsResult.PostOfficeBox = this.registered_office_address.po_box;
            companyDetailsResult.PostCode = this.registered_office_address.postal_code;
            companyDetailsResult.Premises = this.registered_office_address.premises;
            companyDetailsResult.Region = this.registered_office_address.region;

            companyDetailsResult.RegisteredOfficeIsInDispute = (this.registered_office_is_in_dispute ?? false);
            companyDetailsResult.UndeliverableRegisteredOfficeAddress = (this.undeliverable_registered_office_address ?? false);
            companyDetailsResult.DateOfCreation = (this.date_of_creation ?? default(DateTime).ToShortDateString());
            companyDetailsResult.IsActive = (this.company_status == null ? false : (this.company_status == CompanyStatus.Active));

            return companyDetailsResult;
        }
    }

}
 

 
 