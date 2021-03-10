using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;
using SFA.DAS.ApprenticeCommitments.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeQuery
{
    public class ApprenticeshipsQuery : IRequest<List<ApprenticeshipModel>>
    {
        public ApprenticeshipsQuery(Guid Id) => ApprenticeId = Id;

        public Guid ApprenticeId { get; set; }
    }

    public class ApprenticeQueryHandler
        : IRequestHandler<ApprenticeshipsQuery, List<ApprenticeshipModel>>
    {
        private IApprenticeshipRepository _apprenticeshipRepository;

        public ApprenticeQueryHandler(IApprenticeshipRepository apprenticeshipRepository)
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