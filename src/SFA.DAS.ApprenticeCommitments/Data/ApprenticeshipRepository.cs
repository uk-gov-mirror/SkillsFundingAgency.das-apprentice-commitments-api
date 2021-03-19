using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Exceptions;
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

        internal async Task<Apprenticeship> GetById(Guid apprenticeId, long apprenticeshipId)
            => (await Find(apprenticeId, apprenticeshipId))
                ?? throw new DomainException(
                    $"Apprenticeship {apprenticeshipId} for {apprenticeId} not found");

        public async Task<Apprenticeship?> Find(Guid apprenticeId, long apprenticeshipId)
            => await _dbContext.Value.Apprenticeships
                .SingleOrDefaultAsync(
                    a => a.Id == apprenticeshipId &&
                    a.Apprentice.Id == apprenticeId);
    }
}