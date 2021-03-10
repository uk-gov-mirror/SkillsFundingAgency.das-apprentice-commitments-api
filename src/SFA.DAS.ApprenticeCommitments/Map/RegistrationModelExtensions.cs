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
                AccountLegalEntityId = registrationModel.AccountLegalEntityId,
                UserIdentityId = registrationModel.UserIdentityId,
                ApprenticeId = registrationModel.Id
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
                AccountLegalEntityId = registration.AccountLegalEntityId,
                UserIdentityId = registration.UserIdentityId,
            };
        }
    }
}
