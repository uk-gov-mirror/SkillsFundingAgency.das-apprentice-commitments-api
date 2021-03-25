using SFA.DAS.ApprenticeCommitments.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    [Table("Apprenticeship")]
    public class Apprenticeship
    {
#pragma warning disable CS8618 // Constructor for Entity Framework
        private Apprenticeship()
#pragma warning restore CS8618
        {
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
        public long EmployerAccountLegalEntityId { get; private set; }
        public long TrainingProviderId { get; private set; }
        public string TrainingProviderName { get; private set; }
        public Apprentice Apprentice { get; private set; }
        public bool? TrainingProviderCorrect { get; private set; }
        public bool? EmployerCorrect { get; private set; }

        public void ConfirmTrainingProvider(bool trainingProviderCorrect)
        {
            if (TrainingProviderCorrect != null)
            {
                throw new DomainException(
                    "Cannot update TrainingProviderCorrect state more than once");
            }

            TrainingProviderCorrect = trainingProviderCorrect;
        }

        public void ConfirmEmployer(bool employerCorrect)
        {
            if (EmployerCorrect != null)
            {
                throw new DomainException(
                    "Cannot update EmployerCorrect state more than once");
            }

            EmployerCorrect = employerCorrect;
        }
    }
}
