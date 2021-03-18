using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Exceptions;
using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

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

    public class ConfirmTrainingProviderCommandHandler
        : IRequestHandler<ConfirmTrainingProviderCommand>
    {
        private readonly ApprenticeshipRepository _apprenticeships;

        public ConfirmTrainingProviderCommandHandler(
            ApprenticeshipRepository apprenticeships)
            => _apprenticeships = apprenticeships;

        public async Task<Unit> Handle(
            ConfirmTrainingProviderCommand request,
            CancellationToken cancellationToken)
        {
            var apprenticeship =
                await _apprenticeships.Get(request.ApprenticeId, request.ApprenticeshipId)
                ?? throw new DomainException($"Apprenticeship {request.ApprenticeshipId} for {request.ApprenticeId} not found");

            apprenticeship.ConfirmTrainingProvider(request.TrainingProviderCorrect);

            return Unit.Value;
        }
    }
}