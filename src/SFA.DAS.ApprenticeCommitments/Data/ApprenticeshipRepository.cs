using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public class ApprenticeshipRepository
    {
        private readonly Lazy<ApprenticeCommitmentsDbContext> _dbContext;

        public ApprenticeshipRepository(Lazy<ApprenticeCommitmentsDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Apprenticeship>> FindByApprenticeId(Guid apprenticeId)
        {
            var found = await _dbContext.Value.Apprenticeships
                .Where(a => a.Apprentice.Id == apprenticeId).ToListAsync();
            return found;
        }

        public async Task<Apprenticeship?> Get(Guid apprenticeId, long apprenticeshipId)
        {
            var db = _dbContext.Value;
            var match =  await db.Apprenticeships
                .SingleOrDefaultAsync(
                    a => a.Id == apprenticeshipId &&
                    a.Apprentice.Id == apprenticeId);
            return match;
        }
    }
}