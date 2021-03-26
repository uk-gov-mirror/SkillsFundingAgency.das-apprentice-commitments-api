using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;
using System;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.ConfirmTrainingProviderCommand
{
    public class ConfirmTrainingProviderCommand : IUnitOfWorkCommand
    {
        public ConfirmTrainingProviderCommand(
            Guid apprenticeId, long apprenticeshipId,
            bool trainingProviderCorrect)
        {
            ApprenticeId = apprenticeId;
            ApprenticeshipId = apprenticeshipId;
            TrainingProviderCorrect = trainingProviderCorrect;
        }

        public Guid ApprenticeId { get; }
        public long ApprenticeshipId { get; }
        public bool TrainingProviderCorrect { get; }
    }
}