using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeshipQuery
{
    public class ApprenticeshipQueryHandler : IRequestHandler<ApprenticeshipQuery, ApprenticeshipDto>
    {
        private readonly ApprenticeshipRepository _apprenticeshipRepository;

        public ApprenticeshipQueryHandler(ApprenticeshipRepository apprenticeshipRepository)
            => _apprenticeshipRepository = apprenticeshipRepository;

        public async Task<ApprenticeshipDto> Handle(
            ApprenticeshipQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _apprenticeshipRepository
                .Get(request.ApprenticeId, request.ApprenticeshipId);

            return entity.MapToApprenticeshipModel();
        }
    }
}