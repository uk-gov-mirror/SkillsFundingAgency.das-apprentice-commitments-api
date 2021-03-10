using System;
using System.Collections.Generic;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.ApprenticeshipsQuery
{
    public class ApprenticeshipsQuery : IRequest<List<ApprenticeshipModel>>
    {
        public ApprenticeshipsQuery(Guid Id) => ApprenticeId = Id;

        public Guid ApprenticeId { get; set; }
    }
}