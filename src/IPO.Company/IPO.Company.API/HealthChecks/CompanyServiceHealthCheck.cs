using IPO.Company.Interfaces;
using IPO.Company.Models.API;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IPO.Company.API.HealthChecks
{
    public class CompanyServiceHealthCheck : IHealthCheck
    {
        private readonly ICompanyManagementService _companyService;
        private readonly IConfiguration _configuration;

        public CompanyServiceHealthCheck(ICompanyManagementService companyService, IConfiguration configuration)
        {
            _companyService = companyService;
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // App config settings injected by Terraform
            var companyNumbers = 
                _configuration.GetSection("HealthReady")["CompanyNumbers"]!
                .ToString()
                .Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var companyNumber in companyNumbers!)
            {
                var request = new CompanyDetailsRequest
                {
                    CompanyNumber = companyNumber
                };

                try
                {
                    var result = await _companyService.GetCompanyAddress(request);

                    var isHealthy = result != null &&
                        result.CompanyNumber == companyNumber;

                    if (isHealthy)
                    {
                        return HealthCheckResult.Healthy("Company provider is available");
                    }
                }
                catch (Exception)
                {
                }
            }

            return HealthCheckResult.Unhealthy("Company provider is not available");
        }
    }
}