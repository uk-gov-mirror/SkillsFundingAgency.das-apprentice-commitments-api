using System;
using System.Threading.Tasks;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly Lazy<ApprenticeCommitmentsDbContext> _dbContext;

        public RegistrationRepository(Lazy<ApprenticeCommitmentsDbContext> dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(RegistrationModel model)
        {
            await _dbContext.Value.AddAsync(model.MapToRegistration());
        }
    }
}
