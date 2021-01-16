using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SFA.DAS.ApprenticeCommitments.Extensions
{
    public static class DbContextBuilderExtensions
    {
        public static DbContextOptionsBuilder<TContext> UseDataStorage<TContext>(this DbContextOptionsBuilder<TContext> builder, IConfiguration config, string connection) where TContext : DbContext
        {
            if (!config.IsAcceptanceTest())
            {
                return builder.UseSqlServer(connection);
            }

            return builder.UseSqlite(connection);
        }

        public static DbContextOptionsBuilder<TContext> UseDataStorage<TContext>(this DbContextOptionsBuilder<TContext> builder, IConfiguration config, DbConnection connection) where TContext : DbContext
        {
            if (!config.IsAcceptanceTest())
            {
                return builder.UseSqlServer(connection);
            }

            return builder.UseSqlite(connection);
        }
    }
}
