using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Exceptions;
using System;
using System.Threading.Tasks;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public interface IRegistrationContext : IEntityContext<Registration>
    {
        internal async Task<Registration> GetById(Guid registrationId)
            => (await Find(registrationId))
                ?? throw new DomainException($"Registration {registrationId} not found");

        internal async Task<Registration?> Find(Guid registrationId)
            => await Entitites.FirstOrDefaultAsync(x => x.Id == registrationId);

        public async Task<bool> RegistrationsExist()
            => await Entitites.AnyAsync();
    }
}