using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeshipsQuery
{
    public class ApprenticeshipsQueryHandler
        : IRequestHandler<ApprenticeshipsQuery, List<ApprenticeshipDto>>
    {
        private ApprenticeshipRepository _apprenticeshipRepository;

        public ApprenticeshipsQueryHandler(ApprenticeshipRepository apprenticeshipRepository)
            => _apprenticeshipRepository = apprenticeshipRepository;

        public async Task<List<ApprenticeshipDto>> Handle(
            ApprenticeshipsQuery request,
            CancellationToken cancellationToken)
        {
            var apprenticeships = await _apprenticeshipRepository
                .FindByApprenticeIdDb(request.ApprenticeId);
            return apprenticeships
                .Select(ApprenticeshipModelExtensions.MapToApprenticeshipModel)
                .ToList();
        }
    }
}