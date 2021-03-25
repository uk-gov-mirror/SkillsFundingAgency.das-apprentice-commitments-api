using System;
using System.Collections.Generic;
using MediatR;
using SFA.DAS.ApprenticeCommitments.DTOs;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeshipsQuery
{
    public class ApprenticeshipsQuery : IRequest<List<ApprenticeshipDto>>
    {
        public ApprenticeshipsQuery(Guid Id) => ApprenticeId = Id;

        public Guid ApprenticeId { get; set; }
    }
}