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
            ApprenticeshipDetails details)
        {
            CommitmentsApprenticeshipId = commitmentsApprenticeshipId;
            Details = details;
        }

        public long Id { get; private set; }
        public long CommitmentsApprenticeshipId { get; private set; }
        public Apprentice Apprentice { get; private set; }
        public ApprenticeshipDetails Details { get; private set; }

        public bool? TrainingProviderCorrect { get; private set; }
        public bool? EmployerCorrect { get; private set; }
        public bool? ApprenticeshipDetailsCorrect { get; private set; }
        public bool? HowApprenticeshipDeliveredCorrect { get; private set; }

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

        public void ConfirmApprenticeshipDetails(bool apprenticeshipDetailsCorrect)
        {
            if (ApprenticeshipDetailsCorrect != null)
            {
                throw new DomainException(
                    "Cannot update ApprenticeshipDetailsCorrect state more than once");
            }

            ApprenticeshipDetailsCorrect = apprenticeshipDetailsCorrect;
        }

        public void ConfirmHowApprenticeshipWillBeDelivered(bool howApprenticeshipDeliveredCorrect)
        {
            if (HowApprenticeshipDeliveredCorrect != null)
            {
                throw new DomainException(
                    "Cannot update HowApprenticeshipDeliveredCorrect state more than once");
            }

            HowApprenticeshipDeliveredCorrect = howApprenticeshipDeliveredCorrect;
        }
    }
}