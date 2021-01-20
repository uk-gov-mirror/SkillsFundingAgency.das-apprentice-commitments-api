using System;
using System.Data.Common;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Data.Models;

namespace SFA.DAS.ApprenticeCommitments.UnitTests.Infrastructure.SqlServerConnectionFactory
{
    public class WhenConnectingToLocalDatabase
    {
        private ApprenticeCommitments.Infrastructure.SqlServerConnectionFactory _sut;
        private Mock<IConfiguration> _configurationMock;
        private DbContextOptionsBuilder<ApprenticeCommitmentsDbContext> _dbContextOptionsBuilder;
        private string _connectionString;

        [SetUp]
        public void Arrange()
        {
            _configurationMock = new Mock<IConfiguration>();
            _dbContextOptionsBuilder = new DbContextOptionsBuilder<ApprenticeCommitmentsDbContext>();
            _sut = new ApprenticeCommitments.Infrastructure.SqlServerConnectionFactory(_configurationMock.Object);
            _connectionString = "Data Source=(localdb);Initial Catalog=DummyDatabase;Integrated Security=True";
        }

        [TestCase("LOCAL")]
        [TestCase("ACCEPTANCE_TESTS")]
        [TestCase("DEV")]
        public void Then_CreateConnection_should_accept_a_local_connection_string(string environmentName)
        {
            _configurationMock.Setup(x => x[It.IsAny<string>()]).Returns(environmentName);

            var dbConnection = _sut.CreateConnection(_connectionString);
            dbConnection.Should().NotBeNull();
        }

        [Test]
        public void Then_CreateConnection_should_throw_exception_because_connection_string_contains_integrated_security()
        {
            _configurationMock.Setup(x => x[It.IsAny<string>()]).Returns("TEST");

            Func<DbConnection> func = () => _sut.CreateConnection(_connectionString);
            func.Should().Throw<Exception>();
        }


        [TestCase("LOCAL")]
        [TestCase("ACCEPTANCE_TESTS")]
        [TestCase("DEV")]
        public void Then_AddConnection_should_add_sqlConnection_to_builder(string environmentName)
        {
            _configurationMock.Setup(x => x[It.IsAny<string>()]).Returns(environmentName);

            var result = _sut.AddConnection(_dbContextOptionsBuilder, _connectionString);
            result.Should().Be(_dbContextOptionsBuilder);
        }

        [Test]
        public void Then_AddConnection_should_attach_to_existing_Connection_and_return_builder()
        {
            var existingConnection = new SqlConnection(_connectionString);

            var result = _sut.AddConnection(_dbContextOptionsBuilder, existingConnection);
            result.Should().Be(_dbContextOptionsBuilder);
        }
    }
}
