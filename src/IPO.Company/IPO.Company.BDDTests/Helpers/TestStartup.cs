using IPO.Company.API; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration; 
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using IPO.Company.Interfaces;

namespace IPO.Company.BDDTests.Helpers
{

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {

        }   
        public static TestServer GetTestServer()
        {
            var hostBuilder = new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseConfiguration(new ConfigurationBuilder()
                                    .SetBasePath(Environment.CurrentDirectory)
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                    .Build());

            return new TestServer(hostBuilder);
        }
        protected override void AddCompaniesHouseGatewayService(IServiceCollection services)
        {
            services.AddScoped<ICompaniesHouseGateway, MockedCompaniesHouseGateway>();
        }
    }
}
