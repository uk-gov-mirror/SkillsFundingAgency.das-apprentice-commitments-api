using System;
using System.Threading.Tasks;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public class ApprenticeshipRepository : IApprenticeshipRepository
    {
        private readonly Lazy<ApprenticeCommitmentsDbContext> _dbContext;

        public ApprenticeshipRepository(Lazy<ApprenticeCommitmentsDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApprenticeshipModel> Add(ApprenticeshipModel model)
        {
            var apprenticeship = model.MapToApprenticeship();

            var dbContext = _dbContext.Value;

            await dbContext.AddAsync(apprenticeship);
            await dbContext.SaveChangesAsync();

            return apprenticeship.MapToApprenticeshipModel();
        }
    }
}
