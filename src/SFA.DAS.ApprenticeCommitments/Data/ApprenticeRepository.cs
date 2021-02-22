using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public class ApprenticeRepository : IApprenticeRepository
    {
        private readonly Lazy<ApprenticeCommitmentsDbContext> _dbContext;

        public ApprenticeRepository(Lazy<ApprenticeCommitmentsDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApprenticeModel> Add(ApprenticeModel model)
        {
            var apprentice = model.MapToApprentice();
            var dbContext = _dbContext.Value;
            await dbContext.AddAsync(apprentice);
            await dbContext.SaveChangesAsync();

            return apprentice.MapToApprenticeModel();
        }

        public async Task ChangeEmailAddress(long apprenticeId, string email)
        {
            var apprentice = await _dbContext.Value.Apprentices.FindAsync(apprenticeId);
            apprentice.Email = email;
            await _dbContext.Value.SaveChangesAsync();
        }
    }
}
