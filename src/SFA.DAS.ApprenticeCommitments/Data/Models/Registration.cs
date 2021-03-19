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

        public Guid Id { get; set; }
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
        public string EmployerName { get; set; }
        public long EmployerAccountLegalEntityId { get; set; }
        public Guid? UserIdentityId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long TrainingProviderId { get; set; }
        public string TrainingProviderName { get; set; }

        public Guid? ApprenticeId { get; set; }
        public Apprentice? Apprentice { get; private set; }

        public bool HasBeenCompleted => UserIdentityId != null;

        internal void Verify(string firstName, string lastName, MailAddress mailAddress, DateTime dateOfBirth, Guid userIdentityId)
        {
            if (HasBeenCompleted)
                throw new DomainException("Already verified");

            // Verify Email matches incoming email
            if (!mailAddress.ToString().Equals(Email, StringComparison.InvariantCultureIgnoreCase))
                throw new DomainException("Email from Verifying user doesn't match registered user");

            UserIdentityId = userIdentityId;
            AddApprentice(firstName, lastName, mailAddress, dateOfBirth);
        }

        private void AddApprentice(string firstName, string lastName, MailAddress mailAddress, DateTime dateOfBirth)
        {
            Apprentice = new Apprentice(
                Id, firstName, lastName, mailAddress, dateOfBirth);

            Apprentice.AddApprenticeship(new Apprenticeship(
                ApprenticeshipId,
                EmployerAccountLegalEntityId, EmployerName,
                TrainingProviderId, TrainingProviderName));
        }
    }
}