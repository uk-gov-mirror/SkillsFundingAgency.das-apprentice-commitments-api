using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.UnitTests.Application.Commands.CreateRegistrationCommandTests
{
    public class WhenHandlingCreateRegistrationCommand
    {
        private Mock<IRegistrationRepository> _registrationRepositoryMock;
        private CreateRegistrationCommandHandler _sut;

        [SetUp]
        public void Arrange()
        {
            _registrationRepositoryMock = new Mock<IRegistrationRepository>();
            _sut = new CreateRegistrationCommandHandler(_registrationRepositoryMock.Object);
        }

        [Test, AutoData]
        public async Task Then_registration_repository_should_receive_the_registration_model(CreateRegistrationCommand command)
        {
            await _sut.Handle(command, CancellationToken.None);

            _registrationRepositoryMock.Verify(x=>x.Add(It.Is<RegistrationModel>(m=>m.Id == command.RegistrationId && m.ApprenticeshipId == command.ApprenticeshipId && m.Email == command.Email)));
        }
    }
}
