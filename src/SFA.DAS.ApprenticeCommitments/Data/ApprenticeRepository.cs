using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using System;
using System.Linq;
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

        internal async Task<Apprentice> GetById(Guid apprenticeId,
            Action<IQueryable<Apprentice>> addIncludes)
        {
            var query = _db.Apprentices;
            addIncludes(query);
            return await query.SingleOrDefaultAsync(a => a.Id == apprenticeId);
        }
    }
}