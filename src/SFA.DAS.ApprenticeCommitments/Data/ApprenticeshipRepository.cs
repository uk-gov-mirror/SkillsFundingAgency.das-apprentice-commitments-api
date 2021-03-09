using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<ApprenticeshipModel>> FindByApprenticeId(Guid apprenticeId)
        {
            var found = await _dbContext.Value.Apprenticeships
                .Where(a => a.Apprentice.Id == apprenticeId).ToListAsync();
            return found.Select(a => a.MapToApprenticeshipModel()).ToList();
        }
    }
}
