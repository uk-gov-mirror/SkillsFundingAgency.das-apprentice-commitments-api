using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Data.Models;

namespace SFA.DAS.ApprenticeCommitments.UnitTests.Infrastructure.SqlServerConnectionFactory
{
    public class WhenConnectingToAzureDatabase
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
            _connectionString = "Data Source=someserver;Initial Catalog=DummyDatabase;Integrated Security=False";
        }

        [Test]
        public void Then_CreateConnection_should_accept_a_connection_string()
        {
            _configurationMock.Setup(x => x[It.IsAny<string>()]).Returns("LIVE");

            var dbConnection = _sut.CreateConnection(_connectionString);
            dbConnection.Should().NotBeNull();
        }

        [Test]
        public void Then_AddConnection_should_add_sqlConnection_to_builder()
        {
            _configurationMock.Setup(x => x[It.IsAny<string>()]).Returns("PROD");

            var result = _sut.AddConnection(_dbContextOptionsBuilder, _connectionString);
            result.Should().Be(_dbContextOptionsBuilder);
        }
    }
}
