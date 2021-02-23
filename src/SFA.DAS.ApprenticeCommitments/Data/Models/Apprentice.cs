using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    public class Apprentice
    {
        private Apprentice()
        {
        }

        public Apprentice(string firstName, string lastName, Guid userIdentityId, string email, DateTime dateOfBirth)
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
        public string Email { get; private set; }
        public ICollection<ApprenticeEmailHistory> PreviousEmails { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

        internal void UpdateEmail(string email)
        {
            Email = email;
            PreviousEmails.Add(new ApprenticeEmailHistory(email));
        }
    }

    public class ApprenticeEmailHistory
    {
        private ApprenticeEmailHistory()
        {
        }

        public ApprenticeEmailHistory(string emailAddress)
            => EmailAddress = emailAddress;

        public string EmailAddress { get; private set; }
        public DateTime ChangedOn { get; private set; } = DateTime.UtcNow;
    }
}