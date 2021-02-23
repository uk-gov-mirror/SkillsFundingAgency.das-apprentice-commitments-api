using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    public class Apprentice
    {
        private Apprentice()
        {
        }

        public Apprentice(string firstName, string lastName, Guid userIdentityId, MailAddress email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            UserIdentityId = userIdentityId;
            Email = email;
            DateOfBirth = dateOfBirth;
            PreviousEmails = new[] { new ApprenticeEmailHistory(email) };
        }

        public long Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Guid UserIdentityId { get; private set; }
        public MailAddress Email { get; private set; }
        public ICollection<ApprenticeEmailHistory> PreviousEmails { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

        internal void UpdateEmail(MailAddress email)
        {
            Email = email;
            PreviousEmails.Add(new ApprenticeEmailHistory(Email));
        }
    }

    public class ApprenticeEmailHistory
    {
        private ApprenticeEmailHistory()
        {
        }

        public ApprenticeEmailHistory(MailAddress emailAddress)
            => EmailAddress = emailAddress;

        public MailAddress EmailAddress { get; private set; }
        public DateTime ChangedOn { get; private set; } = DateTime.UtcNow;
    }
}