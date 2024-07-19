using IPO.Configuration;

namespace IPO.Company.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
            webBuilder.ConfigureAppConfiguration(configBuilder =>
                   {
                            // Add IPO Azure Configuration - only added when Azure App Config is set
                            configBuilder.AddIPOAzureAppConfigWithManagedIdentity();

                            // Add Template replacement configuration provider to replace templated values
                            configBuilder.AddTemplateConfiguration();
                });

            webBuilder.UseStartup<Startup>();
            });
    }
}
