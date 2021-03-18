using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;
using System;
using System.Linq;
using System.Net.Mail;
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

        public async Task<ApprenticeModel> AddApprentice(
            ApprenticeModel apprentice, ApprenticeshipDto firstApprenticeship)
        {
            var entity = apprentice.MapToApprentice();
            entity.AddApprenticeship(firstApprenticeship.MapToApprenticeship());
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.MapToApprenticeModel();
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

        public async Task ChangeEmailAddress(Guid apprenticeId, MailAddress email)
        {
            var apprentice = await _db.Apprentices
                .Include(a => a.PreviousEmailAddresses)
                .SingleAsync(a => a.Id == apprenticeId);
            apprentice.UpdateEmail(email);
            await _db.SaveChangesAsync();
        }
    }
}