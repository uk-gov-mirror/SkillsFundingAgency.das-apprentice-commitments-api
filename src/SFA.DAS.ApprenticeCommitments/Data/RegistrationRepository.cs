using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Exceptions;
using System;
using System.Threading.Tasks;

#nullable enable

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

        internal async Task<Registration> GetById(Guid registrationId)
            => (await Find(registrationId))
                ?? throw new DomainException($"Registration {registrationId} not found");

        public async Task<Registration?> Find(Guid registrationId)
            => await _dbContext.Value.Registrations
                .FirstOrDefaultAsync(x => x.Id == registrationId);
    }
}