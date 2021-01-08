using System;
using MediatR;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.TestQuery
{
    public class TestQuery : IRequest<int>
    {
        public Guid ApplicationId { get; set; }
    }
}
