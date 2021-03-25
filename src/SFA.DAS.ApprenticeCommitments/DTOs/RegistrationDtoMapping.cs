using SFA.DAS.ApprenticeCommitments.Data.Models;

namespace SFA.DAS.ApprenticeCommitments.DTOs
{
    public static class RegistrationDtoMapping
    {
        public static RegistrationDto MapToRegistrationDto(this Registration registration)
        {
            return new RegistrationDto
            {
                ApprenticeId = registration.ApprenticeId,
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