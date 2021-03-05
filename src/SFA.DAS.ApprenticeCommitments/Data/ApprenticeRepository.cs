using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public class ApprenticeRepository : IApprenticeRepository
    {
        private readonly ApprenticeCommitmentsDbContext _db;

        public ApprenticeRepository(ApprenticeCommitmentsDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<ApprenticeModel> AddApprentice(
            ApprenticeModel apprentice, ApprenticeshipModel firstApprenticeship)
        {
            var entity = apprentice.MapToApprentice();
            entity.AddApprenticeship(firstApprenticeship.MapToApprenticeship());
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.MapToApprenticeModel();
        }

        public async Task ChangeEmailAddress(Guid apprenticeId, MailAddress email)
        {
            var apprentice = await _db.Apprentices
                .Include(a => a.PreviousEmailAddresses)
                .SingleAsync(a => a.UserIdentityId == apprenticeId);
            apprentice.UpdateEmail(email);
            await _db.SaveChangesAsync();
        }
    }
}