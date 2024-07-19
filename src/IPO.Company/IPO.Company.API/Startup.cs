using IPO.Common.API;
using IPO.Company.Gateways;
using IPO.Company.Interfaces;
using IPO.Company.Models.Configuration;
using IPO.Company.Services; 
using System.Net.Http.Headers;  
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.OpenApi.Models;

namespace IPO.Company.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Helper = new IPOStartupHelper("IPO.Company.API", "version");
        }

        public IConfiguration Configuration { get; }
        public IPOStartupHelper Helper { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Helper.AddIPOServicesConfiguration(services); 
            services.AddIPOErrorAwareScoped<ICompanyManagementService, CompanyManagementService>("E002");
            AddCompaniesHouseGatewayService(services);

            services.AddSwaggerGen(config =>
            {

                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Company Microservice", Version = "v1" });
                config.EnableAnnotations();

            });

            AddConfiguration(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Helper.UseIPOConfigurations(app, env);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

        }

        protected virtual void AddCompaniesHouseGatewayService(IServiceCollection services)
        { 
            services.AddIPOErrorAwareHttpClient<ICompaniesHouseGateway, CompaniesHouseGateway>((config) =>
            {
                var settings = new CompaniesHouseApiSettings();
                var settingsSection = Configuration.GetSection(nameof(CompaniesHouseApiSettings));
                settingsSection.Bind(settings, (options) => { options.ErrorOnUnknownConfiguration = true; });
                Validator.ValidateObject(settings, new ValidationContext(settings), validateAllProperties: true);
                config.BaseAddress = settings.BaseAddress;
                config.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Configuration["CompaniesHouseWrapperSubscriptionKey"]);
                config.DefaultRequestHeaders.Add("Accept-Version", Configuration["CompaniesHouseWrapperWrapperVersion"]);
                config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(settings.AcceptMediaType));
                config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(settings.AuthenticationType, 
                                                                                           Convert.ToBase64String(
                                                                                           Encoding.UTF8.GetBytes(settings.APIKey)));

            }, "E003");
        }
        protected virtual void AddConfiguration(IServiceCollection services)
        {
            services.Configure<CompaniesHouseApiSettings>(Configuration);
        }

    }
}

