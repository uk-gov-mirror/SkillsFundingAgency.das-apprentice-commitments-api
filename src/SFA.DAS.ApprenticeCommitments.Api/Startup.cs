using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SFA.DAS.ApprenticeCommitments.Api.Authentication;
using SFA.DAS.ApprenticeCommitments.Api.Configuration;
using SFA.DAS.ApprenticeCommitments.Api.Extensions;

namespace SFA.DAS.ApprenticeCommitments.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var config = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();

            //config.AddAzureTableStorage(options =>
            //{
            //    options.ConfigurationKeys = configuration["ConfigNames"].Split(",");
            //    options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
            //    options.EnvironmentName = configuration["EnvironmentName"];
            //    options.PreFixConfigurationKeys = false;
            //});

#if DEBUG
            config.AddJsonFile($"appsettings.Development.json", optional: true);
#endif
            Configuration = config.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplicationInsightsTelemetry();
            services.AddHealthChecks();
            services.AddSwaggerGen();

            services.Configure<AzureActiveDirectoryConfiguration>(Configuration.GetSection("AzureAd"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<AzureActiveDirectoryConfiguration>>().Value);

            if (!ConfigurationIsLocalOrDev())
            {
                var azureAdConfiguration = Configuration
                    .GetSection("AzureAd")
                    .Get<AzureActiveDirectoryConfiguration>();

                services.AddApiAuthentication(azureAdConfiguration);
            }


            //services.AddEntityFrameworkForEmployerIncentives()
            //    .AddEntityFrameworkUnitOfWork<EmployerIncentivesDbContext>()
            //    .AddNServiceBusClientUnitOfWork();

            //services.AddPersistenceServices();
            //services.AddCommandServices();
            //services.AddQueryServices();
            //services.AddEventServices();

            services
                .AddMvc(o =>
                {
                    if (!ConfigurationIsLocalOrDev())
                    {
                        //o.Conventions.Add(new AuthorizeControllerModelConvention());
                    }
                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("swagger/v1/swagger.json", "Apprentice Commitments API");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection()
                .UseApiGlobalExceptionHandler();

            app.UseRouting();

            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/ping");
            });
        }

        private bool ConfigurationIsLocalOrDev()
        {
            return Configuration["EnvironmentName"].Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
                   Configuration["EnvironmentName"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
