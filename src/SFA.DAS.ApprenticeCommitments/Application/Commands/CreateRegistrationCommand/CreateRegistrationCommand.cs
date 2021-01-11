using System;
using SFA.DAS.ApprenticeCommitments.Infrastructure.MediatorExtensions;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.CreateRegistrationCommand
{
    public class CreateRegistrationCommand : ITransactionCommand
    {
        public Guid RegistrationId { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
    }
}
