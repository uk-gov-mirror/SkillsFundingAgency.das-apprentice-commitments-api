using SFA.DAS.ApprenticeCommitments.Exceptions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    [Table("Apprenticeship")]
    public class Apprenticeship
    {
        public long Id { get; set; }
        public long CommitmentsApprenticeshipId { get; set; }
        public string EmployerName { get; set; }
        public long EmployerAccountLegalEntityId { get; set; }
        public long TrainingProviderId { get; set; }
        public string TrainingProviderName { get; set; }
        public Apprentice Apprentice { get; private set; }
        public bool? TrainingProviderCorrect { get; private set; }

        public void ConfirmTrainingProvider(bool trainingProviderCorrect)
        {
            if (TrainingProviderCorrect != null)
            {
                throw new DomainException(
                    "Cannot update TrainingProviderCorrect state more than once");
            }

            TrainingProviderCorrect = trainingProviderCorrect;
        }
    }
}
