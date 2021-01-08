using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Application.Queries.TestQuery
{
    public class TestQueryHandler : IRequestHandler<TestQuery, int>
    {
        private readonly ApprenticeCommitmentsDbContext _dbContext;

        public TestQueryHandler(Lazy<ApprenticeCommitmentsDbContext> dbContext)
        {
            _dbContext = dbContext.Value;
        }

        public async Task<int> Handle(TestQuery request, CancellationToken cancellationToken)
        {
            var i = 1;
            i++;
            return await Task.FromResult(1);
        }
    }

}
