using Swashbuckle.AspNetCore.Annotations;

namespace IPO.Company.Models.API
{
    [SwaggerSchema(Description = "Response object for the company request")]

    public class CompanyDetailsResult
    {
        [SwaggerSchema(Title = "Name of the company")]
        public string CompanyName { get; set; }

        [SwaggerSchema(Title = "Number of the company")]
        public string CompanyNumber { get; set; }

        [SwaggerSchema(Title = "Creation date of the company")]
        public string DateOfCreation { get; set; }

        [SwaggerSchema(Title = "First line of address")]
        public string AddressLine1 { get; set; }

        [SwaggerSchema(Title = "Second line of address")]
        public string AddressLine2 { get; set; }

        [SwaggerSchema(Title = "Country registered")]
        public string Country { get; set; }

        [SwaggerSchema(Title = "Locality")]
        public string Locality { get; set; }

        [SwaggerSchema(Title = "PO box details")]
        public string PostOfficeBox { get; set; }

        [SwaggerSchema(Title = "Post code registered")]
        public string PostCode { get; set; }

        [SwaggerSchema(Title = "Premises")]
        public string Premises { get; set; }

        [SwaggerSchema(Title = "Region of the company")]
        public string Region { get; set; }

        [SwaggerSchema(Title = "Whether the company is still active")]
        public bool   IsActive { get; set; }

        [SwaggerSchema(Title = "Whether the registered office is in dispute")]
        public bool  RegisteredOfficeIsInDispute { get; set; }

        [SwaggerSchema(Title = "Whether the registered office address is undeliverable")]
        public bool  UndeliverableRegisteredOfficeAddress { get; set; }

    }
}
 