using SFA.DAS.ApprenticeCommitments.Exceptions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    [Table("Registration")]
    public class Registration
    {
#pragma warning disable CS8618 // Private constructor for entity framework
        private Registration()
#pragma warning restore CS8618 
        {
        }

        public Registration(
            Guid registrationId,
            long apprenticeshipId,
            MailAddress email,
            string employerName,
            long employerAccountLegalEntityId,
            long trainingProviderId,
            string trainingProviderName)
        {
            Id = registrationId;
            ApprenticeshipId = apprenticeshipId;
            Email = email.ToString();
            EmployerName = employerName;
            EmployerAccountLegalEntityId = employerAccountLegalEntityId;
            TrainingProviderId = trainingProviderId;
            TrainingProviderName = trainingProviderName;
        }

        public Guid Id { get; private set; }
        public long ApprenticeshipId { get; private set; }
        public string Email { get; private set; }
        public string EmployerName { get; private set; }
        public long EmployerAccountLegalEntityId { get; private set; }
        public Guid? UserIdentityId { get; private set; }
        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
        public long TrainingProviderId { get; private set; }
        public string TrainingProviderName { get; private set; }

        public bool HasBeenCompleted => UserIdentityId != null;

        public Apprentice ConvertToApprentice(string firstName, string lastName, MailAddress emailAddress, DateTime dateOfBirth, Guid userIdentityId)
        {
            EnsureNotAlreadyCompleted();
            EnsureStatedEmailMatchesApproval(emailAddress);

            UserIdentityId = userIdentityId;
            return CreateRegisteredApprentice(firstName, lastName, emailAddress, dateOfBirth);
        }

        private void EnsureNotAlreadyCompleted()
        {
            if (HasBeenCompleted)
                throw new DomainException($"Registration {Id} id already verified");
        }

        private void EnsureStatedEmailMatchesApproval(MailAddress emailAddress)
        {
            if (!emailAddress.ToString().Equals(Email, StringComparison.InvariantCultureIgnoreCase))
                throw new DomainException($"Email from verifying user doesn't match registered user {Id}");
        }

        private Apprentice CreateRegisteredApprentice(string firstName, string lastName, MailAddress emailAddress, DateTime dateOfBirth)
        {
            var apprentice = new Apprentice(
                Id, firstName, lastName, emailAddress, dateOfBirth);

            apprentice.AddApprenticeship(new Apprenticeship(
                ApprenticeshipId,
                EmployerAccountLegalEntityId, EmployerName,
                TrainingProviderId, TrainingProviderName));

            return apprentice;
        }
    }
}