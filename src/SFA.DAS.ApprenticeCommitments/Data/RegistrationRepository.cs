using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public class RegistrationRepository
    {
        private readonly Lazy<ApprenticeCommitmentsDbContext> _dbContext;

        public RegistrationRepository(Lazy<ApprenticeCommitmentsDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> RegistrationsExist()
        {
            var db = _dbContext.Value;
            return await db.Registrations.AnyAsync();
        }

        internal async Task Add(Registration registration)
        {
            await _dbContext.Value.Registrations.AddAsync(registration);
        }

        public async Task<Registration> Get(Guid registrationId)
        {
            var db = _dbContext.Value;
            var entity = await db.Registrations.FirstOrDefaultAsync(x => x.Id == registrationId);

            return entity;
        }
    }
}