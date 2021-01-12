using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.UnitTests.Map.RegistrationModelExtensionsTests
{
    public class WhenMappingToRegistration
    {
        [Test, AutoData]
        public void Then_properties_should_be_mapped_to_registration_entity(RegistrationModel sut)
        {
            var registration = sut.MapToRegistration();

            registration.Id.Should().Be(sut.Id);
            registration.ApprenticeshipId.Should().Be(sut.ApprenticeshipId);
            registration.Email.Should().Be(sut.Email);
        }
    }
}
