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
        public Registration()
        {
            // Private constructor for entity framework
            // Non-nullable field must contain a non-null value when exiting constructor
            Email = "";
            EmployerName = "";
            TrainingProviderName = "";
        }

        public Registration(
            Guid registrationId,
            long apprenticeshipId,
            string email,
            string employerName,
            long employerAccountLegalEntityId,
            long trainingProviderId,
            string trainingProviderName)
        {
            Id = registrationId;
            ApprenticeshipId = apprenticeshipId;
            Email = email;
            EmployerName = employerName;
            EmployerAccountLegalEntityId = employerAccountLegalEntityId;
            TrainingProviderId = trainingProviderId;
            TrainingProviderName = trainingProviderName;
        }

        public Guid Id { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
        public string EmployerName { get; set; }
        public long EmployerAccountLegalEntityId { get; set; }
        public Guid? UserIdentityId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long TrainingProviderId { get; set; }
        public string TrainingProviderName { get; set; }

        public bool HasBeenCompleted => UserIdentityId != null;

        internal Apprentice Verify(string firstName, string lastName, MailAddress emailAddress, DateTime dateOfBirth, Guid userIdentityId)
        {
            EnsureNotAlreadyCompleted();
            EnsureStatedEmailMatchesApproval(emailAddress);

            UserIdentityId = userIdentityId;
            return CreateRegisteredApprentice(firstName, lastName, emailAddress, dateOfBirth);
        }

        private void EnsureNotAlreadyCompleted()
        {
            if (HasBeenCompleted)
                throw new DomainException("Already verified");
        }

        private void EnsureStatedEmailMatchesApproval(MailAddress emailAddress)
        {
            if (!emailAddress.ToString().Equals(Email, StringComparison.InvariantCultureIgnoreCase))
                throw new DomainException("Email from verifying user doesn't match registered user");
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