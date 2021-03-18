using SFA.DAS.ApprenticeCommitments.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    [Table("Apprenticeship")]
    public class Apprenticeship
    {
        private Apprenticeship()
        {
            // for Entity Framework
        }

        public Apprenticeship(long commitmentsApprenticeshipId,
            long employerAccountLegalEntityId,
            string employerName,
            long trainingProviderId,
            string trainingProviderName)
        {
            CommitmentsApprenticeshipId = commitmentsApprenticeshipId;
            EmployerAccountLegalEntityId = employerAccountLegalEntityId;
            EmployerName = employerName;
            TrainingProviderId = trainingProviderId;
            TrainingProviderName = trainingProviderName;
        }

        public long Id { get; private set; }
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
