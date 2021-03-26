using System;
using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.ConfirmEmployerCommand
{
    public class ConfirmEmployerCommand : IUnitOfWorkCommand
    {
        public ConfirmEmployerCommand(
            Guid apprenticeId, long apprenticeshipId,
            bool trainingProviderCorrect)
        {
            ApprenticeId = apprenticeId;
            ApprenticeshipId = apprenticeshipId;
            EmployerCorrect = trainingProviderCorrect;
        }

        public Guid ApprenticeId { get; }
        public long ApprenticeshipId { get; }
        public bool EmployerCorrect { get; }
    }
}