using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.DTOs;

namespace SFA.DAS.ApprenticeCommitments.Map
{
    public static class RegistrationDtoMapping
    {
        public static RegistrationDto MapToRegistrationDto(this Registration registration)
        {
            return new RegistrationDto
            {
                Id = registration.Id,
                ApprenticeshipId = registration.ApprenticeshipId,
                Email = registration.Email,
                EmployerName = registration.EmployerName,
                EmployerAccountLegalEntityId = registration.EmployerAccountLegalEntityId,
                UserIdentityId = registration.UserIdentityId,
                TrainingProviderId = registration.TrainingProviderId,
                TrainingProviderName = registration.TrainingProviderName,
            };
        }
    }
}