using System;
using System.Threading.Tasks;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;

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
    }
}
