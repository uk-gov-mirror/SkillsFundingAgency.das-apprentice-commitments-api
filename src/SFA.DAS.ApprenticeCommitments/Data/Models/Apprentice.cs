using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    public class Apprentice
    {
        private Apprentice()
        {
            // for Entity Framework
        }

        public Apprentice(string firstName, string lastName, Guid userIdentityId, MailAddress email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            UserIdentityId = userIdentityId;
            Email = email;
            DateOfBirth = dateOfBirth;
            PreviousEmailAddresses = new[] { new ApprenticeEmailAddressHistory(email) };
        }

        public long Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Guid UserIdentityId { get; private set; }
        public MailAddress Email { get; private set; }
        public ICollection<ApprenticeEmailAddressHistory> PreviousEmailAddresses { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

        internal void UpdateEmail(MailAddress email)
        {
            Email = email;
            PreviousEmailAddresses.Add(new ApprenticeEmailAddressHistory(Email));
        }
    }

    public class ApprenticeEmailAddressHistory
    {
        private ApprenticeEmailAddressHistory()
        {
        }

        public ApprenticeEmailAddressHistory(MailAddress emailAddress)
            => EmailAddress = emailAddress;

        public MailAddress EmailAddress { get; private set; }
        public DateTime ChangedOn { get; private set; } = DateTime.UtcNow;
    }
}