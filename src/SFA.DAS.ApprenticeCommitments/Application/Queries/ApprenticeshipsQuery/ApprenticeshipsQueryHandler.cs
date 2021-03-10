using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeshipsQuery
{
    public class ApprenticeshipsQueryHandler
        : IRequestHandler<ApprenticeshipsQuery, List<ApprenticeshipModel>>
    {
        private IApprenticeshipRepository _apprenticeshipRepository;

        public ApprenticeshipsQueryHandler(IApprenticeshipRepository apprenticeshipRepository)
            => _apprenticeshipRepository = apprenticeshipRepository;

        public async Task<List<ApprenticeshipModel>> Handle(
            ApprenticeshipsQuery request,
            CancellationToken cancellationToken)
        {
            var apprenticeships = await _apprenticeshipRepository
                .FindByApprenticeId(request.ApprenticeId);
            return apprenticeships;
        }
    }
}