using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;
using System;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class ChangeEmailAddressCommand : IUnitOfWorkCommand
    {
        public Guid ApprenticeId { get; set; }
        public string Email { get; set; }
    }
}
