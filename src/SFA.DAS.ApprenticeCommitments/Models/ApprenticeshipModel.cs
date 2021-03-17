﻿using SFA.DAS.ApprenticeCommitments.Data.Models;
using System;

namespace SFA.DAS.ApprenticeCommitments.Models
{
    public class ApprenticeshipModel
    {
        public long? Id { get; set; }
        public long CommitmentsApprenticeshipId { get; set; }
        public string EmployerName { get; set; }
        public long EmployerAccountLegalEntityId { get; set; }
        public long TrainingProviderId { get; internal set; }
        public string TrainingProviderName { get; set; }
        public bool TrainingProviderCorrect { get; private set; }

        internal void ConfirmTrainingProvider(bool trainingProviderCorrect)
            => TrainingProviderCorrect = trainingProviderCorrect;
    }
}
