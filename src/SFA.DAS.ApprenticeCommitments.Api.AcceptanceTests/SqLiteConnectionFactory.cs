using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Infrastructure;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests
{
    public class SqLiteConnectionFactory : IConnectionFactory
    {
        public DbContextOptionsBuilder<TContext> AddConnection<TContext>(DbContextOptionsBuilder<TContext> builder, string connection) where TContext : DbContext
        {
            return builder.UseSqlite(connection);
        }

        public DbContextOptionsBuilder<TContext> AddConnection<TContext>(DbContextOptionsBuilder<TContext> builder, DbConnection connection) where TContext : DbContext
        {
            return builder.UseSqlite(connection);
        }

        public DbConnection CreateConnection(string connection)
        {
            return new SqliteConnection(connection);
        }
    }
}