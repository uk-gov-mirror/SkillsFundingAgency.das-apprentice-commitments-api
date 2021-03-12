using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeshipQuery
{
    public class ApprenticeshipQueryHandler : IRequestHandler<ApprenticeshipQuery, ApprenticeshipModel>
    {
        private readonly IApprenticeshipRepository _apprenticeshipRepository;

        public ApprenticeshipQueryHandler(IApprenticeshipRepository apprenticeshipRepository)
            => _apprenticeshipRepository = apprenticeshipRepository;

        public Task<ApprenticeshipModel> Handle(
            ApprenticeshipQuery request,
            CancellationToken cancellationToken)
        {
            return _apprenticeshipRepository.Get(request.ApprenticeId, request.ApprenticeshipId);
        }
    }
}