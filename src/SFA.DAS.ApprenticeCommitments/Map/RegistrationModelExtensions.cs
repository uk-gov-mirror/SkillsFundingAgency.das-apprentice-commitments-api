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
                UserIdentityId = registrationModel.UserIdentityId,
                ApprenticeId = registrationModel.ApprenticeId
            };
        }

        public static RegistrationModel MapToRegistrationModel(this Registration registration)
        {
            return new RegistrationModel
            {
                Id = registration.Id,
                ApprenticeshipId = registration.ApprenticeshipId,
                Email = registration.Email,
                UserIdentityId = registration.UserIdentityId,
                ApprenticeId = registration.ApprenticeId
            };
        }
    }
}
