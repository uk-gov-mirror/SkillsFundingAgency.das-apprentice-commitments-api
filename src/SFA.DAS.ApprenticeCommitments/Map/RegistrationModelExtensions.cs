using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Map
{
    public static class RegistrationModelExtensions
    {
        public static Registration MapToRegistration(this RegistrationModel registrationModel)
        {
            return new Registration
            {
                Id = registrationModel.Id,
                ApprenticeshipId = registrationModel.ApprenticeshipId,
                Email = registrationModel.Email,
                EmployerName = registrationModel.EmployerName,
                EmployerAccountLegalEntityId = registrationModel.EmployerAccountLegalEntityId,
                UserIdentityId = registrationModel.UserIdentityId,
                ApprenticeId = registrationModel.Id,
                TrainingProviderName = registrationModel.TrainingProviderName,
            };
        }

        public static RegistrationModel MapToRegistrationModel(this Registration registration)
        {
            return new RegistrationModel
            {
                Id = registration.Id,
                ApprenticeshipId = registration.ApprenticeshipId,
                Email = registration.Email,
                EmployerName = registration.EmployerName,
                EmployerAccountLegalEntityId = registration.EmployerAccountLegalEntityId,
                UserIdentityId = registration.UserIdentityId,
            };
        }
    }
}
