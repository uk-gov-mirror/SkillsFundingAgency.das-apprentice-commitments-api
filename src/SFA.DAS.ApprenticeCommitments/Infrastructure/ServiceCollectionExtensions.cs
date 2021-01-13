using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NServiceBus;
using NServiceBus.ObjectBuilder.MSDependencyInjection;
using NServiceBus.Persistence;
using SFA.DAS.ApprenticeCommitments.Configuration;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Models;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.AzureServiceBus;
using SFA.DAS.NServiceBus.Configuration.MicrosoftDependencyInjection;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;
using SFA.DAS.NServiceBus.Hosting;
using SFA.DAS.NServiceBus.SqlServer.Configuration;
using SFA.DAS.NServiceBus.SqlServer.Data;
using SFA.DAS.UnitOfWork.Context;
using SFA.DAS.UnitOfWork.NServiceBus.Configuration;

namespace SFA.DAS.ApprenticeCommitments.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkForApprenticeCommitments(this IServiceCollection services)
        {
            //return services;
            return services.AddScoped(p =>
            {
                var unitOfWorkContext = p.GetService<IUnitOfWorkContext>();
                ApprenticeCommitmentsDbContext dbContext;
                try
                {
                    var synchronizedStorageSession = unitOfWorkContext.Get<SynchronizedStorageSession>();
                    var sqlStorageSession = synchronizedStorageSession.GetSqlStorageSession();
                    var optionsBuilder = new DbContextOptionsBuilder<ApprenticeCommitmentsDbContext>().UseSqlServer(sqlStorageSession.Connection);
                    dbContext = new ApprenticeCommitmentsDbContext(optionsBuilder.Options);
                    dbContext.Database.UseTransaction(sqlStorageSession.Transaction);
                }
                catch (KeyNotFoundException)
                {
                    var settings = p.GetService<IOptions<ApplicationSettings>>().Value;
                    var optionsBuilder = new DbContextOptionsBuilder<ApprenticeCommitmentsDbContext>().UseSqlServer(settings.DbConnectionString);
                    dbContext = new ApprenticeCommitmentsDbContext(optionsBuilder.Options);
                }

                return dbContext;
            });
        }


        public static async Task<UpdateableServiceProvider> StartNServiceBus(this UpdateableServiceProvider serviceProvider, IConfiguration configuration)
        {
            var endpointConfiguration = new EndpointConfiguration("SFA.DAS.ApprenticeCommitments.Api")
                .UseMessageConventions()
                .UseNewtonsoftJsonSerializer()
                .UseOutbox(true)
                .UseServicesBuilder(serviceProvider)
                .UseSqlServerPersistence(() => new SqlConnection(configuration["ApplicationSettings:DbConnectionString"]))
                .UseUnitOfWork();

            if (configuration["ApplicationSettings:NServiceBusConnectionString"].Equals("UseLearningEndpoint=true", StringComparison.CurrentCultureIgnoreCase) || string.IsNullOrEmpty(configuration["NServiceBusConnectionString"]))
            {
                endpointConfiguration.UseTransport<LearningTransport>();
            }
            else
            {
                endpointConfiguration.UseAzureServiceBusTransport(configuration["ApplicationSettings:NServiceBusConnectionString"]);
            }

            if (!string.IsNullOrEmpty(configuration["ApplicationSettings:NServiceBusLicense"]))
            {
                endpointConfiguration.License(configuration["ApplicationSettings:NServiceBusLicense"]);
            }

            var endpoint = await Endpoint.Start(endpointConfiguration);

            serviceProvider.AddSingleton(p => endpoint)
                .AddSingleton<IMessageSession>(p => p.GetService<IEndpointInstance>())
                .AddHostedService<NServiceBusHostedService>();

            return serviceProvider;
        }

    }
}
