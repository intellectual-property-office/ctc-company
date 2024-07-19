using IPO.Common.Infrastructure; 

namespace IPO.Company.Gateways.Models
{
    public static class StatusCodeExceptionFactory
    {
        public static StatusCodeException CreateNotFoundStatusCodeException<T>( string errorCode, string companyNumber)
        {
            var error = Error.Create<T>(errorCode);
            error.Description += $"Company Address details for the company number({companyNumber}) were not found.";
            return new StatusCodeException(error, "Record not found", null, 422);
        }
    }
}
