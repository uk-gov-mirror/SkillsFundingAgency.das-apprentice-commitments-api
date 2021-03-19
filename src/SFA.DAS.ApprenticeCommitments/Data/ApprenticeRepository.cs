using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Exceptions;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public class ApprenticeRepository
    {
        private readonly ApprenticeCommitmentsDbContext _db;

        public ApprenticeRepository(ApprenticeCommitmentsDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<Apprentice> AddApprenticeDb(Apprentice apprentice)
        {
            await _db.AddAsync(apprentice);
            await _db.SaveChangesAsync(); // to populate the ID
            return apprentice;
        }

        internal async Task<Apprentice> GetById(Guid apprenticeId)
            => await Find(apprenticeId)
                    ?? throw new DomainException(
                        $"Apprentice {apprenticeId} not found");

        private async Task<Apprentice> Find(Guid apprenticeId)
            => await _db.Apprentices
                .SingleOrDefaultAsync(a => a.Id == apprenticeId);
    }
}