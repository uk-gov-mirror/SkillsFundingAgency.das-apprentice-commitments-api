using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SFA.DAS.ApprenticeCommitments.Infrastructure;

namespace SFA.DAS.ApprenticeCommitments.Extensions
{
    public static class DbContextBuilderExtensions
    {
        public static DbContextOptionsBuilder<TContext> UseDataStorage<TContext>(
            this DbContextOptionsBuilder<TContext> builder, IConnectionFactory connectionFactory, string connection)
            where TContext : DbContext
        {
            return connectionFactory.AddConnection(builder, connection);
        }

        public static DbContextOptionsBuilder<TContext> UseDataStorage<TContext>(
            this DbContextOptionsBuilder<TContext> builder, IConnectionFactory connectionFactory, DbConnection connection)
            where TContext : DbContext
        {
            return connectionFactory.AddConnection(builder, connection);
        }
    }
}
