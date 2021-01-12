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
                Email = registrationModel.Email
            };
        }
    }
}
