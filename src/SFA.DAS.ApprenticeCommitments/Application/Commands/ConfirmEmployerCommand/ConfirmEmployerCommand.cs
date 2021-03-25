using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Infrastructure.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.ConfirmTrainingProviderCommand
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

    public class ConfirmEmployerCommandHandler
        : IRequestHandler<ConfirmEmployerCommand>
    {
        private readonly IApprenticeshipContext _apprenticeships;

        public ConfirmEmployerCommandHandler(IApprenticeshipContext apprenticeships)
            => _apprenticeships = apprenticeships;

        public async Task<Unit> Handle(ConfirmEmployerCommand request, CancellationToken _)
        {
            var apprenticeship = await _apprenticeships.GetById(request.ApprenticeId, request.ApprenticeshipId);
            apprenticeship.ConfirmEmployer(request.EmployerCorrect);
            return Unit.Value;
        }
    }
}