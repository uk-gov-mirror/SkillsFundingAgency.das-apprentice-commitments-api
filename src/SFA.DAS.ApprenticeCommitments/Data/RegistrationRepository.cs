using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Map;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public class RegistrationRepository
    {
        private readonly Lazy<ApprenticeCommitmentsDbContext> _dbContext;

        public RegistrationRepository(Lazy<ApprenticeCommitmentsDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(RegistrationDto model)
        {
            await _dbContext.Value.AddAsync(model.MapToRegistration());
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

        public async Task<RegistrationDto> Get(Guid registrationId)
        {
            var db = _dbContext.Value;
            var entity = await db.Registrations.FirstOrDefaultAsync(x=>x.Id == registrationId);

            return entity?.MapToRegistrationModel();
        }

        public async Task<Registration> GetDb(Guid registrationId)
        {
            var db = _dbContext.Value;
            var entity = await db.Registrations.FirstOrDefaultAsync(x => x.Id == registrationId);

            return entity;
        }
    }
}
