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

        public async Task<ApprenticeModel> Add(ApprenticeModel model)
        {
            var apprentice = model.MapToApprentice();
            await _db.AddAsync(apprentice);
            await _db.SaveChangesAsync();
            return apprentice.MapToApprenticeModel();
        }

        public async Task ChangeEmailAddress(long apprenticeId, string email)
        {
            var apprentice = await _db.Apprentices
                .Include(a => a.PreviousEmails)
                .SingleAsync(a => a.Id == apprenticeId);
            apprentice.UpdateEmail(email);
            await _db.SaveChangesAsync();
        }
    }
}