using IPO.Company.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using IPO.Company.Interfaces;
using Microsoft.Extensions.Hosting;

namespace IPO.Company.BDDTests.Helpers
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public static TestServer GetTestServer()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost
                        .UseTestServer()
                        .UseStartup<TestStartup>();
                });
            var host = hostBuilder.Start();
            return host.GetTestServer();
        }

        protected override void AddCompaniesHouseGatewayService(IServiceCollection services)
        {
            services.AddScoped<ICompaniesHouseGateway, MockedCompaniesHouseGateway>();
        }
    }
}