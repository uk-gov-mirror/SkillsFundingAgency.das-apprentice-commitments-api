﻿using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Map
{
    public static class RegistrationModelExtensions
    {
        public static Registration MapToRegistration(this RegistrationDto registrationModel)
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
                TrainingProviderId = registrationModel.TrainingProviderId,
                TrainingProviderName = registrationModel.TrainingProviderName,
            };
        }

        public static RegistrationDto MapToRegistrationModel(this Registration registration)
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
