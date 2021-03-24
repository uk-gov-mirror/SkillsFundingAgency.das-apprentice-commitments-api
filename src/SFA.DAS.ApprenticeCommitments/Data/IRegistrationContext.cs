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
        internal async Task<Registration> GetById(Guid apprenticeId)
            => (await Find(apprenticeId))
                ?? throw new DomainException($"Registration for Apprentice {apprenticeId} not found");

        internal async Task<Registration?> Find(Guid apprenticeId)
            => await Entities.FirstOrDefaultAsync(x => x.ApprenticeId == apprenticeId);

        public async Task<bool> RegistrationsExist()
            => await Entities.AnyAsync();
    }
}