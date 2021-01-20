using System.Data.Common;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SFA.DAS.ApprenticeCommitments.Extensions;

namespace SFA.DAS.ApprenticeCommitments.Infrastructure
{
    public class SqlServerConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlServerConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbContextOptionsBuilder<TContext> AddConnection<TContext>(DbContextOptionsBuilder<TContext> builder, string connection) where TContext : DbContext
        {
            return builder.UseSqlServer(CreateConnection(connection));
        }

        public DbContextOptionsBuilder<TContext> AddConnection<TContext>(DbContextOptionsBuilder<TContext> builder, DbConnection connection) where TContext : DbContext
        {
            return builder.UseSqlServer(connection);
        }

        public DbConnection CreateConnection(string connection)
        {
            var sqlConnection = new SqlConnection(connection)
            {
                AccessToken = GetAccessToken(),
            };

            return sqlConnection;
        }

        private string GetAccessToken()
        {
            if (_configuration.IsLocalAcceptanceOrDev())
            {
                return null;
            }

            return new AzureServiceTokenProvider()
                .GetAccessTokenAsync("https://database.windows.net/")
                .GetAwaiter().GetResult();
        }
    }
}