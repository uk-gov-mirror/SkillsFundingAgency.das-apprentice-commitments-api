using System;
using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class CreateRegistrationCommand : IUnitOfWorkCommand
    {
        public Guid RegistrationId { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
        public string EmployerName { get; set; }
        public long AccountLegalEntityId { get; set; }
    }
}
