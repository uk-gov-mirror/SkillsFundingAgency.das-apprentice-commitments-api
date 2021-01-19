using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.ApprenticeCommitments.Infrastructure
{
    public class SqlServerConnectionFactory : IConnectionFactory
    {
        public DbContextOptionsBuilder<TContext> AddConnection<TContext>(DbContextOptionsBuilder<TContext> builder, string connection) where TContext : DbContext
        {
            return builder.UseSqlServer(connection);
        }

        public DbContextOptionsBuilder<TContext> AddConnection<TContext>(DbContextOptionsBuilder<TContext> builder, DbConnection connection) where TContext : DbContext
        {
            return builder.UseSqlServer(connection);
        }

        public DbConnection CreateConnection(string connection)
        {
            return new SqlConnection(connection);
        }
    }
}