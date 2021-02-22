using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class ChangeEmailAddressCommand : IUnitOfWorkCommand
    {
        public long ApprenticeId { get; set; }
        public string Email { get; set; }
    }
}
