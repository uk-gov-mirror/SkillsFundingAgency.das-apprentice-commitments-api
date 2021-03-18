using System;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeshipQuery
{
    public class ApprenticeshipQuery : IRequest<ApprenticeshipDto>
    {
        public ApprenticeshipQuery(Guid apprenticeId, long apprenticeshipId)
        {
            ApprenticeId = apprenticeId;
            ApprenticeshipId = apprenticeshipId;
        }

        public Guid ApprenticeId { get; }
        public long ApprenticeshipId { get; }
    }
}