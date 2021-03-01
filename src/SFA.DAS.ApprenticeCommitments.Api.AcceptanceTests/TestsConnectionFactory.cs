using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Infrastructure;
using System.Data.Common;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests
{
    public class TestsDbConnectionFactory : IConnectionFactory
    {
        private static IConnectionFactory factory { get; }
        public static string ConnectionString { get; }// => $"Data Source={AcceptanceTestsData.AcceptanceTestsDatabaseName}";

        static TestsDbConnectionFactory()
        {
            //*
            factory = new SqliteTestsConnectionFactory();
            ConnectionString = $"Data Source={AcceptanceTestsData.AcceptanceTestsDatabaseName}";
            /*/
            factory = new SqlServerTestsConnectionFactory();
            ConnectionString = "Data Source=.;Initial Catalog=SFA.DAS.ApprenticeCommitments.AcceptanceTests;Integrated Security=True;";
            //*/
        }

        public DbContextOptionsBuilder<TContext> AddConnection<TContext>(
            DbContextOptionsBuilder<TContext> builder) where TContext : DbContext
        {
            return factory.AddConnection(builder, ConnectionString);
        }

        DbContextOptionsBuilder<TContext> IConnectionFactory.AddConnection<TContext>(
            DbContextOptionsBuilder<TContext> builder, string connection)
        {
            return factory.AddConnection(builder, connection);
        }

        DbContextOptionsBuilder<TContext> IConnectionFactory.AddConnection<TContext>(
            DbContextOptionsBuilder<TContext> builder, DbConnection connection)
        {
            return factory.AddConnection(builder, connection);
        }

        DbConnection IConnectionFactory.CreateConnection(string connection)
        {
            return factory.CreateConnection(connection);
        }
    }

    internal class SqliteTestsConnectionFactory : IConnectionFactory
    {
        DbContextOptionsBuilder<TContext> IConnectionFactory.AddConnection<TContext>(
            DbContextOptionsBuilder<TContext> builder, string connection)
        {
            return builder.UseSqlite(connection);
        }

        DbContextOptionsBuilder<TContext> IConnectionFactory.AddConnection<TContext>(
            DbContextOptionsBuilder<TContext> builder, DbConnection connection)
        {
            return builder.UseSqlite(connection);
        }

        DbConnection IConnectionFactory.CreateConnection(string connection)
        {
            return new SqliteConnection(connection);
        }
    }

    internal class SqlServerTestsConnectionFactory : IConnectionFactory
    {
        DbContextOptionsBuilder<TContext> IConnectionFactory.AddConnection<TContext>(
            DbContextOptionsBuilder<TContext> builder, string connection)
        {
            return builder.UseSqlServer(connection);
        }

        DbContextOptionsBuilder<TContext> IConnectionFactory.AddConnection<TContext>(
            DbContextOptionsBuilder<TContext> builder, DbConnection connection)
        {
            return builder.UseSqlServer(connection);
        }

        DbConnection IConnectionFactory.CreateConnection(string connection)
        {
            return new SqliteConnection(connection);
        }
    }
}