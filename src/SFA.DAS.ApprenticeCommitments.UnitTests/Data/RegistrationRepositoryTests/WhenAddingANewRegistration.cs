using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.UnitTests.Data.RegistrationRepositoryTests
{
    public class WhenAddingANewRegistration
    {
        private ApprenticeCommitmentsDbContext _dbContext;
        private RegistrationRepository _sut;

        [SetUp]
        public void Arrange()
        {
            var options = new DbContextOptionsBuilder<ApprenticeCommitmentsDbContext>()
                .UseInMemoryDatabase("ApprenticeCommitmentsDbContext" + Guid.NewGuid()).Options;
            _dbContext = new ApprenticeCommitmentsDbContext(options);

            _sut = new RegistrationRepository(new Lazy<ApprenticeCommitmentsDbContext>(_dbContext));
        }

        [Test, AutoData]
        public async Task Then_registration_should_be_saved_in_dbContext(RegistrationModel registrationModel)
        {
            await _sut.Add(registrationModel);

            await _dbContext.SaveChangesAsync();

            var registration = await _dbContext.Registrations.SingleOrDefaultAsync();

            registration.Should().NotBeNull();
            registration.Id.Should().Be(registrationModel.Id);
            registration.ApprenticeshipId.Should().Be(registrationModel.ApprenticeshipId);
            registration.Email.Should().Be(registrationModel.Email);
        }
    }
}
