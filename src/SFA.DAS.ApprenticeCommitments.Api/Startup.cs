using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NServiceBus.ObjectBuilder.MSDependencyInjection;
using SFA.DAS.ApprenticeCommitments.Api.Authentication;
using SFA.DAS.ApprenticeCommitments.Api.Extensions;
using SFA.DAS.ApprenticeCommitments.Configuration;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Extensions;
using SFA.DAS.ApprenticeCommitments.Infrastructure;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.UnitOfWork.EntityFrameworkCore.DependencyResolution.Microsoft;
using SFA.DAS.UnitOfWork.NServiceBus.Features.ClientOutbox.DependencyResolution.Microsoft;

namespace SFA.DAS.ApprenticeCommitments.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var config = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();

            if (!Configuration.IsAcceptanceTest())
            {
                config.AddAzureTableStorage(options =>
                {
                    options.ConfigurationKeys = configuration["ConfigNames"].Split(",");
                    options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
                    options.EnvironmentName = configuration["EnvironmentName"];
                    options.PreFixConfigurationKeys = false;
                });
#if DEBUG
                config.AddJsonFile($"appsettings.Development.json", optional: true);
#endif
            }

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

            if (!Configuration.IsLocalAcceptanceOrDev())
            {
                var azureAdConfiguration = Configuration
                    .GetSection("AzureAd")
                    .Get<AzureActiveDirectoryConfiguration>();

                services.AddApiAuthentication(azureAdConfiguration);
            }

            services.AddOptions();
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            if (Configuration.IsAcceptanceTest())
            {
                services.AddEntityFrameworkSqlite();
            }

            services.AddEntityFrameworkForApprenticeCommitments(Configuration)
                .AddEntityFrameworkUnitOfWork<ApprenticeCommitmentsDbContext>()
                .AddNServiceBusClientUnitOfWork();

            //if (Configuration.IsAcceptanceTest())
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<ApprenticeCommitmentsDbContext>().UseSqlite(Configuration["ApplicationSettings:DbConnectionString"]);
            //    var dbContext = new ApprenticeCommitmentsDbContext(optionsBuilder.Options);

            //    dbContext.Registrations.Add(new Registration
            //        { ApprenticeshipId = 100, Email = "paul@1222", Id = Guid.NewGuid() });
            //    dbContext.SaveChanges();
            //}




            services.AddServicesForApprenticeCommitments();

            services
                .AddMvc(o =>
                {
                    if (!Configuration.IsLocalAcceptanceOrDev())
                    {
                        o.Filters.Add(new AuthorizeFilter(PolicyNames.Default));
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

        public void ConfigureContainer(UpdateableServiceProvider serviceProvider)
        {
            serviceProvider.StartNServiceBus(Configuration).GetAwaiter().GetResult();
        }
    }
}
